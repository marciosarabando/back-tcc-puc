using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Models.Response;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Api.Controllers
{
    [Authorize]
    [Controller]
    [Route("v1/estabelecimento")]
    public class EstabelecimentoController : ControllerBase
    {
        private readonly IEstabelecimentoRepository _estabelecimento_repository;
        private readonly IMapper _mapper;

        public EstabelecimentoController(IEstabelecimentoRepository estabelecimento_repository, IMapper mapper)
        {
            _estabelecimento_repository = estabelecimento_repository;
            _mapper = mapper;
        }

        [HttpGet("listar-todos")]
        public List<EstabelecimentoResponse> GetAll()
        {
            return _mapper.Map<List<EstabelecimentoResponse>>(_estabelecimento_repository.ObterTodos());
        }
    }
}