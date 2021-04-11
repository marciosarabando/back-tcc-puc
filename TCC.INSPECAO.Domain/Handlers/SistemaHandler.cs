using System;
using System.Collections.Generic;
using AutoMapper;
using Flunt.Notifications;
using TCC.INSPECAO.Domain.Commands;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Commands.Sistema;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Handlers.Contracts;
using TCC.INSPECAO.Domain.Models.Response;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Handlers
{
    public class SistemaHandler : Notifiable,
                                    IHandler<CriarSistemaCommand>,
                                    IHandler<AlterarItemSistemaCommand>
    {
        private readonly string _msgError = "Ops, parece que deu algum problema";
        private readonly ISistemaRepository _sistema_repository;
        private readonly IEstabelecimentoRepository _estabelecimento_repository;
        private readonly ISistemaItemRepository _sistema_item_repository;
        private readonly IUnidadeMedidaRepository _unidade_medida_repository;
        private readonly IUsuarioRepository _usuario_repository;
        private readonly IMapper _mapper;

        public SistemaHandler(ISistemaRepository sistema_repository,
                            IEstabelecimentoRepository estabelecimento_repository,
                            ISistemaItemRepository sistema_item_repository,
                            IUnidadeMedidaRepository unidade_medida_repository,
                            IUsuarioRepository usuario_repository,
                            IMapper mapper)
        {
            _sistema_repository = sistema_repository;
            _estabelecimento_repository = estabelecimento_repository;
            _sistema_item_repository = sistema_item_repository;
            _unidade_medida_repository = unidade_medida_repository;
            _usuario_repository = usuario_repository;
            _mapper = mapper;
        }

        public ICommandResult Handle(CriarSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var usuario = _usuario_repository.ObterPorIdFirebase(command.IdFirebase);

            var estabelecimento = _estabelecimento_repository.ObterPorId(usuario.Estabelecimento.Id);

            var ultimaOrdem = _sistema_repository.ObterUltimaOrdem();

            var sistema = new Sistema(command.Nome.ToUpper(), command.Descricao, ultimaOrdem + 1, estabelecimento);

            _sistema_repository.Criar(sistema);

            foreach (var item in command.ItensSistema)
            {
                var unidadeMedida = _unidade_medida_repository.ObterPorId(Guid.Parse(item.IdUnidadeMedida));

                var ultimaOrdemItem = _sistema_item_repository.ObterUltimaOrdem(sistema.Id);

                _sistema_item_repository.Criar(new Entity.SistemaItem(item.Nome.ToUpper(), item.Descricao, sistema, ultimaOrdemItem + 1, unidadeMedida));
            }

            //Retorna o resultado
            return new GenericCommandResult(true, "Sistema criado com Sucesso!", null);
        }

        public ICommandResult Handle(ListarItensSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var itensSistema = _sistema_item_repository.ObterItensSistemaInspecao(command.IdSistema);

            //Retorna o resultado
            return new GenericCommandResult(true, "Itens do sistema encontrado com Sucesso!", _mapper.Map<List<ItensSistemaResponse>>(itensSistema));
        }

        public ICommandResult Handle(ListarSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var usuario = _usuario_repository.ObterPorIdFirebase(command.IdFirebase);

            var sistemas = _sistema_repository.ObterSistemasPorEstabelecimento(usuario.Estabelecimento.Id);

            //Retorna o resultado
            return new GenericCommandResult(true, "Relação de Sistema encontrada com Sucesso!", _mapper.Map<List<SistemaResponse>>(sistemas));
        }

        public ICommandResult Handle(ListarDetalhesSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var sistema = _sistema_repository.ObterDetalhesPorId(command.IdSistema);

            //Retorna o resultado
            return new GenericCommandResult(true, "Relação de Sistema encontrada com Sucesso!", _mapper.Map<SistemaDetalheResponse>(sistema));
        }

        public ICommandResult Handle(AtualizarSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var sistema = _sistema_repository.ObterPorId(Guid.Parse(command.Id));

            sistema.AlterarNomeDescricao(command.Nome, command.Descricao);
            sistema.AtivarDesativar(command.Ativo);

            _sistema_repository.Atualizar(sistema);

            //Atualiza os Itens de Inspeção
            foreach (var item in command.ItensSistema)
            {
                var itemSistema = new SistemaItem();
                var unidadeMedida = _unidade_medida_repository.ObterPorId(Guid.Parse(item.IdUnidadeMedida));

                if (Guid.TryParse(item.Id, out var idItem))
                {
                    itemSistema = _sistema_item_repository.ObterPorId(idItem);
                    itemSistema.AlterarSistemaItem(item.Nome, item.Descricao, unidadeMedida);
                    itemSistema.AtualizarOrdem(item.NumeroOrdem);
                    itemSistema.AtivarDesativar(item.Ativo);
                    _sistema_item_repository.Atualizar(itemSistema);
                }
                else
                {
                    var ultimaOrdemItem = _sistema_item_repository.ObterUltimaOrdem(sistema.Id);
                    _sistema_item_repository.Criar(new SistemaItem(item.Nome, item.Descricao, sistema, ultimaOrdemItem + 1, unidadeMedida));
                }
            }

            //Retorna o resultado
            return new GenericCommandResult(true, "Sistema alterado com Sucesso!", null);
        }

        public ICommandResult Handle(CriarItemSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var sistema = _sistema_repository.ObterPorId(command.IdSistema);

            var unidade_medida = _unidade_medida_repository.ObterPorId(command.IdUnidadeMedida);

            var ultimaOrdemItem = _sistema_item_repository.ObterUltimaOrdem(sistema.Id);

            var item_sistema = new SistemaItem(command.Nome,
                                                command.Descricao,
                                                sistema,
                                                ultimaOrdemItem + 1,
                                                unidade_medida);

            _sistema_item_repository.Criar(item_sistema);

            //Retorna o resultado
            return new GenericCommandResult(true, "Item de Sistema criado com Sucesso!", null);
        }

        public ICommandResult Handle(AlterarItemSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var item_sistema = _sistema_item_repository.ObterPorId(command.IdItemSistema);

            var unidade_medida = _unidade_medida_repository.ObterPorId(command.IdUnidadeMedida);

            item_sistema.AlterarSistemaItem(command.Nome, command.Descricao, unidade_medida);

            _sistema_item_repository.Atualizar(item_sistema);

            //Retorna o resultado
            return new GenericCommandResult(true, "Item de Sistema alterado com Sucesso!", null);
        }

        public ICommandResult Handle(AtivarDesativarSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var sistema = _sistema_repository.ObterPorId(command.IdSistema);

            sistema.AtivarDesativar(command.Ativo);

            _sistema_repository.Atualizar(sistema);

            //Retorna o resultado
            return new GenericCommandResult(true, "Sistema alterado com Sucesso!", null);
        }

        public ICommandResult Handle(AtivarDesativarItemSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var item_sistema = _sistema_item_repository.ObterPorId(command.IdItemSistema);

            item_sistema.AtivarDesativar(command.Ativo);

            _sistema_item_repository.Atualizar(item_sistema);

            //Retorna o resultado
            return new GenericCommandResult(true, "Item do Sistema alterado com Sucesso!", null);
        }

        public ICommandResult Handle(AtualizarOrdemSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            foreach (var item in command.SistemasOrdem)
            {
                var sistema = _sistema_repository.ObterPorId(item.IdSistema);
                sistema.AtualizarOrdem(item.NumeroOrdem);
                _sistema_repository.Atualizar(sistema);
            }

            //Retorna o resultado
            return new GenericCommandResult(true, "Ordem atualizada com Sucesso!", null);
        }

        public ICommandResult Handle(AtualizarOrdemItemSistemaCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            foreach (var item in command.ItensSistemasOrdem)
            {
                var item_sistema = _sistema_item_repository.ObterPorId(item.IdItemSistema);
                item_sistema.AtualizarOrdem(item.NumeroOrdem);
                _sistema_item_repository.Atualizar(item_sistema);
            }

            //Retorna o resultado
            return new GenericCommandResult(true, "Ordem atualizada com Sucesso!", null);
        }

    }
}