using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.INSPECAO.Domain.Commands;
using TCC.INSPECAO.Domain.Commands.Inspecao;
using TCC.INSPECAO.Domain.Handlers;
using TCC.INSPECAO.Domain.Models.Response.Relatorio;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Api.Controllers
{
    [Controller]
    [Authorize]
    [Route("v1/inspecao")]
    public class InspecaoController : ControllerBase
    {
        private readonly IInspecaoRepository _inspecao_repository;
        private readonly ISistemaRepository _sistema_repository;
        private readonly IMapper _mapper;

        public InspecaoController(IInspecaoRepository inspecao_repository,
        IMapper mapper,
        ISistemaRepository sistema_repository)
        {
            _inspecao_repository = inspecao_repository;
            _mapper = mapper;
            _sistema_repository = sistema_repository;
        }


        [Route("iniciar")]
        [HttpPost]
        public GenericCommandResult Iniciar([FromBody] IniciarInspecaoCommand command, [FromServices] InspecaoHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new IniciarInspecaoCommand(
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value
            ));
        }

        [Route("{idInspecao}/listar-sistemas")]
        [HttpGet]
        public GenericCommandResult GetSistemasRondaInspecao([FromRoute] Guid idInspecao, [FromServices] InspecaoHandler handler, [FromServices] IInspecaoRepository inspecao_repository)
        {
            return (GenericCommandResult)handler.Handle(new ListarSistemasInspecaoCommand(
                idInspecao,
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
                inspecao_repository
            ));
        }

        [Route("{idInspecao}/listar-itens-inspecao")]
        [HttpPost]
        //[ProducesResponseTypeAttribute(ItensInspecaoAndamentoResponse)]
        public GenericCommandResult GetItensSistemaRondaInspecao([FromRoute] Guid idInspecao,
                                                                [FromBody] ListarItensSistemaInpenspecaoAndamentoCommand command,
                                                                [FromServices] InspecaoHandler handler,
                                                                [FromServices] IInspecaoRepository inspecao_repository)
        {
            return (GenericCommandResult)handler.Handle(new ListarItensSistemaInpenspecaoAndamentoCommand(
                idInspecao,
                command.IdSistema,
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
                inspecao_repository
            ));
        }

        [Route("{idInspecao}/salvar-item-inspecao")]
        [HttpPost]
        public GenericCommandResult PostSalvarItemSistemaRondaInspecao([FromRoute] Guid idInspecao,
                                                                [FromBody] GravarItemSistemaInspecaoCommand command,
                                                                [FromServices] InspecaoHandler handler,
                                                                [FromServices] IInspecaoRepository inspecao_repository)
        {
            return (GenericCommandResult)handler.Handle(new GravarItemSistemaInspecaoCommand(
                idInspecao,
                command.IdItemSistema,
                command.Valor,
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
                inspecao_repository
            ));
        }

        [Route("{idInspecao}/finalizar")]
        [HttpPost]
        public GenericCommandResult PostFinalizarRondaInspecao([FromRoute] Guid idInspecao,
                                                                [FromBody] FinalizarInspecaoCommand command,
                                                                [FromServices] InspecaoHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new FinalizarInspecaoCommand(
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
                idInspecao,
                command.Observacao
            ));
        }

        [Route("possui-inspecao-andamento")]
        [HttpGet]
        public GenericCommandResult GetVerificaSeUsuarioPossuiInspecaoAndamento([FromServices] InspecaoHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new ListarInspecaoAndamentoUsuarioCommand(
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value
            ));
        }

        [Route("relatorio/listar-por-periodo")]
        [HttpGet]
        public IEnumerable<RelatorioInspecaoResponse> GetRelatorioPorPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            var resp = _inspecao_repository.ObterRelatorioPeriodo(inicio, fim);

            return _mapper.Map<IEnumerable<RelatorioInspecaoResponse>>(resp);
        }

        [Route("relatorio/listar-por-usuario")]
        [HttpGet]
        public IEnumerable<RelatorioInspecaoResponse> GetRelatorioPorUsuario([FromQuery] Guid idUsuario)
        {
            var resp = _inspecao_repository.ObterRelatorioPorUsuario(idUsuario);

            return _mapper.Map<IEnumerable<RelatorioInspecaoResponse>>(resp);
        }


        [Route("relatorio/listar-anos-disponiveis")]
        [HttpGet]
        public IEnumerable<int> GetListaAnosDisponiveis()
        {
            return _inspecao_repository.ObterAnosDisponiveisInspecao();
        }

        [Route("relatorio/listar-usuarios-disponiveis")]
        [HttpGet]
        public IEnumerable<Object> GetListaUsuariosDisponiveis()
        {
            return _inspecao_repository.ObterUsuariosDisponiveisInspecao(x => new { x.Usuario.Id, x.Usuario.Nome });
        }

        [Route("relatorio/listar-detalhes-inspecao")]
        [HttpGet]
        public RelatorioInspecaoDetalhesResponse GetRelatorioDetalhesInspecao([FromQuery] Guid idInspecao)
        {
            var resp = _inspecao_repository.ObterRelatorioInspecaoDetalhes(idInspecao);
            var sistemas = _sistema_repository.ObterTodos();

            var relatorioInspecaoDetalhesResponse = new RelatorioInspecaoDetalhesResponse
            {
                DataHoraFim = resp.DataHoraFim,
                DataHoraInicio = resp.DataHoraInicio,
                IdInspecao = resp.Id,
                Observacao = resp.Observacao,
                Status = resp.InspecaoStatus.Nome,
                Turno = resp.Turno.Nome,
                Usuario = resp.Usuario.Nome
            };

            var sistemaInspecaoResponse = new List<SistemaInspecaoResponse>();

            foreach (var sistema in sistemas)
            {
                var sistemaResponse = new SistemaInspecaoResponse();
                var listaItensSistemaInspecaoResponse = new List<ItensSistemaInspecaoResponse>();

                sistemaResponse.IdSistema = sistema.Id;
                sistemaResponse.NomeSistema = sistema.Nome;

                var itensDoSistema = resp.InspecaoItem.Where(x => x.SistemaItem.Sistema.Id == sistema.Id);
                foreach (var item in itensDoSistema)
                {
                    listaItensSistemaInspecaoResponse.Add(new ItensSistemaInspecaoResponse
                    {
                        DataHora = item.DataHora,
                        Nome = item.SistemaItem.Nome,
                        Descricao = item.SistemaItem.Descricao,
                        Valor = item.Valor,
                        UnidadeMedida = item.SistemaItem.UnidadeMedida.Nome,
                    });
                }
                sistemaResponse.ItensInspecao = listaItensSistemaInspecaoResponse;
                sistemaInspecaoResponse.Add(sistemaResponse);
            }

            relatorioInspecaoDetalhesResponse.SistemaInspecaoResponse = sistemaInspecaoResponse;

            return relatorioInspecaoDetalhesResponse;
        }
    }
}