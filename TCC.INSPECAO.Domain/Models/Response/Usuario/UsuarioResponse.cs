using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Estabelecimento { get; set; }
        public bool Ativo { get; set; }
        public List<Claims> Claims { get; set; }
    }
}