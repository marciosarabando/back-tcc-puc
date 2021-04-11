using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Flunt.Notifications;
using TCC.INSPECAO.Domain.Commands.Usuario;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Handlers.Contracts;
using TCC.INSPECAO.Domain.Models.Response;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Domain.Commands;

namespace TCC.INSPECAO.Domain.Handlers
{
    public class UsuarioHandler : Notifiable,
                                    IHandler<RegistrarUsuarioCommand>,
                                    IHandler<LogarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuario_repository;
        private readonly IEstabelecimentoRepository _estabelecimento_repository;
        private readonly IClaimsRepository _claims_repository;
        private readonly IUsuarioClaimsRepository _usuario_claims_repository;
        private readonly IMapper _mapper;

        private readonly string _msgError = "Ops, parece que deu algum problema";

        public UsuarioHandler(IUsuarioRepository usuario_repository,
                                IEstabelecimentoRepository estabelecimento_repository,
                                IClaimsRepository claims_repository,
                                IUsuarioClaimsRepository usuario_claims_repository,
                                IMapper mapper)
        {
            _usuario_repository = usuario_repository;
            _estabelecimento_repository = estabelecimento_repository;
            _claims_repository = claims_repository;
            _usuario_claims_repository = usuario_claims_repository;
            _mapper = mapper;
        }

        public ICommandResult Handle(RegistrarUsuarioCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            //Verifica se o e-mail do usuário já existe
            var usuario = _usuario_repository.ObterPorEmail(command.Email);

            if (usuario != null)
                return new GenericCommandResult(false, "O e-mail informado já está em uso no Sistema", command.Notifications);

            //Busca o estabelecimento provisório
            var estabelecimento = _estabelecimento_repository.ObterPorCNPJ(command.CnpjEstabelecimento);

            //Criar o Usuario
            var novo_usuario = new Usuario(estabelecimento, command.IdFirebase, command.Email, command.Nome);

            //Salvar o Usuario
            _usuario_repository.Criar(novo_usuario);

            //Busca a Claim Visitante
            var claimVisitante = _claims_repository.ObterPorNomeValor("PerfilAcesso", "Visitante");

            //Vincular a claim ao usuario
            _usuario_claims_repository.Criar(new UsuarioClaims
            {
                UsuarioId = novo_usuario.Id,
                ClaimId = claimVisitante.Id
            });

            //Retorna o resultado
            return new GenericCommandResult(true, "Usuario Registrado com Sucesso!", _mapper.Map<UsuarioResponse>(novo_usuario));
        }

        public ICommandResult Handle(LogarUsuarioCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var usuario_claim = _usuario_claims_repository.ObterClaimsUsuario(command.IdFirebase);

            if (usuario_claim.Count == 0)
                return new GenericCommandResult(false, "Não foi possível encontrar o usuário: " + command.Email + ".", command.Notifications);


            //Retorna o resultado
            return new GenericCommandResult(true, "Usuario logado com Sucesso!", _mapper.Map<UsuarioResponse>(usuario_claim));
        }

        public ICommandResult Handle(AlterarUsuarioCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var usuario = _usuario_repository.ObterPorId(command.IdUsuario);

            usuario.AtivarDesativar(command.Ativo);

            _usuario_repository.Atualizar(usuario);

            //Busca a claim informada no request
            var claim = _claims_repository.ObterPorNomeValor("PerfilAcesso", command.Perfil);

            var claimUsuario = _usuario_claims_repository.ObterClaimsUsuario(usuario.IdFirebase).FirstOrDefault();

            _usuario_claims_repository.Remover(claimUsuario);

            //Vincular a claim ao usuario
            _usuario_claims_repository.Criar(new UsuarioClaims
            {
                UsuarioId = usuario.Id,
                ClaimId = claim.Id
            });

            //Retorna o resultado
            return new GenericCommandResult(true, "Usuario alterado com Sucesso!", null);
        }
    }
}