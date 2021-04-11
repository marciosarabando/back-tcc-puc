using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.INSPECAO.Domain.Commands;
using TCC.INSPECAO.Domain.Models.Response;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Api.Controllers
{
    [Controller]
    [Authorize]
    [Route("v1/unidade-medida")]
    public class UnidadeMedidaController : ControllerBase
    {
        [HttpGet]
        public List<UnidadeMedidaResponse> Listar([FromServices] IUnidadeMedidaRepository _unidadeMedidaRepository, [FromServices] IMapper _mapper)
        {
            return _mapper.Map<List<UnidadeMedidaResponse>>(_unidadeMedidaRepository.ObterTodos());
        }
    }
}