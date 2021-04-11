using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.INSPECAO.Domain.Commands;
using TCC.INSPECAO.Domain.Commands.Usuario;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Handlers;
using TCC.INSPECAO.Domain.Models.Response;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Api.Controllers
{
    [Controller]
    [Authorize]
    [Route("v1/auth")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [ClaimsAuthorize("PerfilAcesso", "Administrador")]
        [HttpGet("obter-usuarios")]
        public List<UsuarioResponse> GetAll()
        {
            var usuarios = _usuarioRepository.ObterTodos();
            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }

        [ClaimsAuthorize("PerfilAcesso", "Administrador")]
        [HttpGet("obter-usuario")]
        public UsuarioResponse GetUsuarioById([FromQuery] Guid idUsuario)
        {
            var usuario = _usuarioRepository.ObterPorId(idUsuario);
            return _mapper.Map<UsuarioResponse>(usuario);
        }

        [Route("cadastro-inicial")]
        [HttpPost]
        public GenericCommandResult Create([FromBody] RegistrarUsuarioCommand command, [FromServices] UsuarioHandler handler)
        {
            command.IdFirebase = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value;
            command.Email = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("email")).Value;

            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("login")]
        [HttpPost]
        public GenericCommandResult Login([FromServices] UsuarioHandler handler)
        {
            var idUserFirebase = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value;
            var email = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("email")).Value;

            return (GenericCommandResult)handler.Handle(new LogarUsuarioCommand
            {
                IdFirebase = idUserFirebase,
                Email = email
            });
        }

        [ClaimsAuthorize("PerfilAcesso", "Administrador")]
        [Route("alterar-usuario")]
        [HttpPut]
        public GenericCommandResult AlterarUsuario([FromServices] UsuarioHandler handler, [FromBody] AlterarUsuarioCommand usuario)
        {
            return (GenericCommandResult)handler.Handle(new AlterarUsuarioCommand
            {
                IdUsuario = usuario.IdUsuario,
                Perfil = usuario.Perfil,
                Ativo = usuario.Ativo
            });
        }
    }
}