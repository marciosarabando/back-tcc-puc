using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.INSPECAO.Domain.Commands;
using TCC.INSPECAO.Domain.Commands.Sistema;
using TCC.INSPECAO.Domain.Handlers;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Api.Controllers
{
    [Controller]
    [Authorize]
    [Route("v1/sistema")]
    public class SistemaController : ControllerBase
    {

        [Route("criar")]
        [HttpPost]
        public GenericCommandResult Iniciar([FromBody] CriarSistemaCommand command, [FromServices] SistemaHandler handler, [FromServices] IEstabelecimentoRepository estabelecimentoRepository)
        {
            return (GenericCommandResult)handler.Handle(new CriarSistemaCommand(
                HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
                command.Nome,
                command.Descricao,
                command.ItensSistema,
                estabelecimentoRepository
            ));
        }

        [Route("alterar")]
        [HttpPut]
        public GenericCommandResult Alterar([FromBody] AtualizarSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] ISistemaRepository sistema_repository)
        {
            return (GenericCommandResult)handler.Handle(new AtualizarSistemaCommand(
            HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
            command.Id,
            command.Nome,
            command.Descricao,
            command.Ativo,
            command.ItensSistema
            ));
        }

        [Route("listar")]
        [HttpGet]
        public GenericCommandResult Listar([FromServices] SistemaHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new ListarSistemaCommand
            {
                IdFirebase = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value
            });
        }

        [Route("listar-detalhes")]
        [HttpGet]
        public GenericCommandResult ListarDetalhes([FromQuery] Guid idSistema, [FromServices] SistemaHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new ListarDetalhesSistemaCommand
            {
                IdFirebase = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value,
                IdSistema = idSistema
            });
        }

        [Route("atualizar-ordem")]
        [HttpPost]
        public GenericCommandResult AtualizarOrdem([FromBody] AtualizarOrdemSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] ISistemaRepository sistema_repository)
        {
            return (GenericCommandResult)handler.Handle(new AtualizarOrdemSistemaCommand(command.SistemasOrdem,
            sistema_repository));
        }

        [Route("ativar-desativar")]
        [HttpPost]
        public GenericCommandResult AtivarDesativar([FromBody] AtivarDesativarSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] ISistemaRepository sistema_repository)
        {
            return (GenericCommandResult)handler.Handle(new AtivarDesativarSistemaCommand(command.IdSistema,
            command.Ativo,
            sistema_repository));
        }



        [Route("item/criar")]
        [HttpPost]
        public GenericCommandResult CriarItemSistema([FromBody] CriarItemSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] ISistemaRepository sistema_repository,
        [FromServices] IUnidadeMedidaRepository unidade_medida_repository
        )
        {
            return (GenericCommandResult)handler.Handle(new CriarItemSistemaCommand(
                command.Nome,
                command.Descricao,
                command.IdUnidadeMedida,
                command.IdSistema,
                sistema_repository,
                unidade_medida_repository
            ));
        }

        [Route("item/alterar")]
        [HttpPut]
        public GenericCommandResult AlterarItemSistema([FromBody] AlterarItemSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] IUnidadeMedidaRepository unidade_medida_repository,
        [FromServices] ISistemaItemRepository sistema_item_repository)
        {
            return (GenericCommandResult)handler.Handle(new AlterarItemSistemaCommand(command.IdItemSistema,
            command.Nome,
            command.Descricao,
            command.IdUnidadeMedida,
            unidade_medida_repository,
            sistema_item_repository));
        }

        [Route("item/listar")]
        [HttpGet]
        public GenericCommandResult ListarItens([FromQuery] Guid idSistema, [FromServices] SistemaHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new ListarItensSistemaCommand
            {
                IdSistema = idSistema
            });
        }

        [Route("item/atualizar-ordem")]
        [HttpPost]
        public GenericCommandResult AtualizarOrdemItem([FromBody] AtualizarOrdemItemSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] ISistemaItemRepository sistema_item_repository)
        {
            return (GenericCommandResult)handler.Handle(new AtualizarOrdemItemSistemaCommand(command.ItensSistemasOrdem,
            sistema_item_repository));
        }

        [Route("item/ativar-desativar")]
        [HttpPost]
        public GenericCommandResult AtivarDesativarItemSistema([FromBody] AtivarDesativarItemSistemaCommand command,
        [FromServices] SistemaHandler handler,
        [FromServices] ISistemaItemRepository sistema_item_repository)
        {
            return (GenericCommandResult)handler.Handle(new AtivarDesativarItemSistemaCommand(command.IdItemSistema,
            command.Ativo,
            sistema_item_repository));
        }

    }
}