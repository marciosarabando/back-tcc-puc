using System.Collections.Generic;
using AutoMapper;
using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.INSPECAO.Domain.Models.Response.Claim;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Api.Controllers
{
    [Controller]
    [Authorize]
    [Route("v1/claim")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimsRepository _claimRepository;
        private readonly IMapper _mapper;

        public ClaimController(IClaimsRepository claimRepository, IMapper mapper)
        {
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        [ClaimsAuthorize("PerfilAcesso", "Administrador")]
        [HttpGet("obter-claims")]
        public List<ClaimResponse> GetAll()
        {
            var claims = _claimRepository.ObterTodos();
            return _mapper.Map<List<ClaimResponse>>(claims);
        }



    }
}