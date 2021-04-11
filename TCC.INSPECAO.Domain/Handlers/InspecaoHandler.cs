using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Flunt.Notifications;
using TCC.INSPECAO.Domain.Commands;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Commands.Inspecao;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Handlers.Contracts;
using TCC.INSPECAO.Domain.Models.Response;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Handlers
{
    public class InspecaoHandler : Notifiable,
                                    IHandler<IniciarInspecaoCommand>

    {
        private readonly IInspecaoStatusRepository _inspecao_status_repository;
        private readonly IUsuarioRepository _usuario_repository;
        private readonly IInspecaoRepository _inspecao_repository;
        private readonly ITurnoRepository _turno_repository;
        private readonly ISistemaRepository _sistema_repository;
        private readonly IInspecaoItemRepository _inspecao_item_repository;
        private readonly ISistemaItemRepository _sistema_item_repository;
        private readonly IMapper _mapper;
        private readonly string _msgError = "Ops, parece que deu algum problema";

        public InspecaoHandler(IInspecaoRepository inspecao_repository,
                                IUsuarioRepository usuario_repository,
                                IMapper mapper,
                                IInspecaoStatusRepository inspecao_status_repository,
                                ITurnoRepository turno_repository,
                                ISistemaRepository sistema_repository,
                                IInspecaoItemRepository inspecao_item_repository,
                                ISistemaItemRepository sistema_item_repository)
        {
            _usuario_repository = usuario_repository;
            _inspecao_repository = inspecao_repository;
            _mapper = mapper;
            _inspecao_status_repository = inspecao_status_repository;
            _turno_repository = turno_repository;
            _sistema_repository = sistema_repository;
            _inspecao_item_repository = inspecao_item_repository;
            _sistema_item_repository = sistema_item_repository;
        }

        public ICommandResult Handle(IniciarInspecaoCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            //Obtem usuário
            var usuario = _usuario_repository.ObterPorIdFirebase(command.IdFirebase);

            //Status da Inspecao
            var inspecaoStatus = _inspecao_status_repository.ObterPorNome("INICIADA");

            //Turno
            var turno = _turno_repository.ObterPorHorario(DateTime.Now);

            //Cria uma nova inspecao
            var nova_inspecao = new Inspecao(usuario.Estabelecimento, DateTime.Now, null, null, turno, usuario, inspecaoStatus);

            //Salvar a Inspecao
            _inspecao_repository.Criar(nova_inspecao);

            //Retorna o resultado
            return new GenericCommandResult(true, "Inspeção iniciada com Sucesso!", _mapper.Map<InspecaoResponse>(nova_inspecao));
        }

        public ICommandResult Handle(ListarSistemasInspecaoCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var response = new List<SistemasRondaResponse>();

            var usuario = _usuario_repository.ObterPorIdFirebase(command.IdFirebase);

            var sistemas = _sistema_repository.ObterSistemasPorEstabelecimento(usuario.Estabelecimento.Id).Where(x => x.Ativo == true);

            foreach (var sistema in sistemas)
            {
                var itens_inspecao = _inspecao_item_repository.ObterItensInspecao(command.IdInspecao, sistema.Id);

                var resp = new SistemasRondaResponse();
                resp.IdSistema = sistema.Id;
                resp.Nome = sistema.Nome;
                resp.Descricao = sistema.Descricao;
                resp.Status = "Concluída";
                resp.InspecaoConcluida = true;

                if (itens_inspecao.Count > 0)
                {
                    foreach (var sistemaItem in sistema.SistemaItens)
                    {
                        if (!itens_inspecao.Select(x => x.SistemaItem).Contains(sistemaItem) && sistemaItem.Ativo == true)
                        {
                            resp.Status = "Em andamento";
                            resp.InspecaoConcluida = false;
                        }
                    }
                }
                else
                {
                    resp.Status = "Não iniciada";
                    resp.InspecaoConcluida = false;
                }

                response.Add(resp);
            }

            //Retorna o resultado
            return new GenericCommandResult(true, "Lista de Sistema da Inspeção encontrada com Sucesso!", response);
        }

        public ICommandResult Handle(ListarInspecaoAndamentoUsuarioCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var usuario = _usuario_repository.ObterPorIdFirebase(command.IdFirebase);

            var inspecaoAndamento = _inspecao_repository.ObterInspecaoEmAndamentoPorUsuario(usuario.Id);

            var response = new InspecaoAndamentoResponse();
            if (inspecaoAndamento != null)
            {
                response.idInspecao = inspecaoAndamento.Id;
                response.DataHoraInicio = inspecaoAndamento.DataHoraInicio;
                response.PossuiInspecaoAndamento = true;
            }
            else
            {
                response.PossuiInspecaoAndamento = false;
            }


            //Retorna o resultado
            return new GenericCommandResult(true, "Consulta de Inspeção em andamento realizada com Sucesso!", response);
        }

        public ICommandResult Handle(ListarItensSistemaInpenspecaoAndamentoCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var response = new List<ItensInspecaoAndamentoResponse>();

            var itens_sistema = _sistema_item_repository.ObterItensSistemaInspecao(command.IdSistema);

            if (!itens_sistema.Any())
                return new GenericCommandResult(false, "Itens de Inspeção não encontrados! Entre em contato com o administador do sistema.", null);

            var itens = new List<ItensInspecao>();
            var nomeSistema = itens_sistema.FirstOrDefault();

            foreach (var item_sistema in itens_sistema)
            {
                var item_inspecao = _inspecao_item_repository.ObterItemInspecao(command.IdInspecao, item_sistema.Id);

                itens.Add(new ItensInspecao
                {
                    Id = item_sistema.Id,
                    Nome = item_sistema.Nome,
                    Descricao = item_sistema.Descricao,
                    UnidadeMedida = item_sistema.UnidadeMedida.Nome,
                    TipoDado = item_sistema.UnidadeMedida.TipoDado,
                    Valor = item_inspecao != null ? item_inspecao.Valor : ""
                });
            }

            response.Add(new ItensInspecaoAndamentoResponse
            {
                NomeSistema = nomeSistema.Sistema.Nome,
                ItensInspecao = itens
            });

            //Retorna o resultado
            return new GenericCommandResult(true, "Consulta de Itens da Inspeção em andamento realizada com Sucesso!", response);
        }

        public ICommandResult Handle(GravarItemSistemaInspecaoCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var itemInspecao = _inspecao_item_repository.ObterItemInspecao(command.IdInspecao, command.IdItemSistema);

            if (itemInspecao != null)
            {
                itemInspecao.AtualizaValor(command.Valor);
                _inspecao_item_repository.AtualizarItemInspecao(itemInspecao);
            }
            else
            {
                var inspecao = _inspecao_repository.ObterPorId(command.IdInspecao);
                var item_sistema = _sistema_item_repository.ObterPorId(command.IdItemSistema);

                _inspecao_item_repository.CriarItemInspecao(new InspecaoItem(inspecao, DateTime.Now, null, item_sistema, command.Valor));
                itemInspecao = _inspecao_item_repository.ObterItemInspecao(command.IdInspecao, command.IdItemSistema);
            }

            //Verificar se todos os itens já foram inspecionados
            var itens_sistema = _sistema_item_repository.ObterItensSistemaInspecao(itemInspecao.SistemaItem.Sistema.Id);

            var itens_inspecao = _inspecao_item_repository.ObterItensInspecao(command.IdInspecao, itemInspecao.SistemaItem.Sistema.Id);

            var inspecaoConcluida = true;

            foreach (var item in itens_sistema)
            {
                if (!itens_inspecao.Any(x => x.SistemaItem.Nome.Contains(item.Nome)))
                {
                    inspecaoConcluida = false;
                }
            }

            //Retorna o resultado
            return new GenericCommandResult(true, "Item de Inspeção salvo com Sucesso!", new { inspecaoConcluida = inspecaoConcluida });
        }

        public ICommandResult Handle(FinalizarInspecaoCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, _msgError, command.Notifications);

            var usuario = _usuario_repository.ObterPorIdFirebase(command.IdFirebase);

            var inspecao = _inspecao_repository.ObterInspecaoEmAndamentoPorUsuario(usuario.Id);

            //Verifica se a inspeção pertence ao usuário
            if (inspecao == null)
                return new GenericCommandResult(false, "Não foi possível localizar a inspeção em andamento para o usuário logado", null);

            var inspecaoStatus = _inspecao_status_repository.ObterPorNome("FINALIZADA");

            inspecao.AlterarStatus(inspecaoStatus);
            inspecao.FinalizarInspecao(command.Observacao);

            _inspecao_repository.Atualizar(inspecao);

            //Retorna o resultado
            return new GenericCommandResult(true, "Inspeção finalizada com Sucesso!", null);
        }
    }
}