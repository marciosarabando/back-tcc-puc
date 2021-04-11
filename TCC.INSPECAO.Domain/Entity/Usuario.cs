using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TCC.INSPECAO.Domain.Entity
{
    public class Usuario : Entity
    {
        public Usuario()
        {

        }

        public Usuario(Estabelecimento estabelecimento, string idFirebase, string email, string nome)
        {
            Estabelecimento = estabelecimento;
            IdFirebase = idFirebase;
            Email = email;
            Nome = nome;
            Ativo = true;
        }
        public Estabelecimento Estabelecimento { get; private set; }
        public string IdFirebase { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; set; }

        [JsonIgnore]
        public virtual List<UsuarioClaims> UsuarioClaims { get; private set; }

        public void AtivarDesativar(bool ativo)
        {
            Ativo = ativo;
        }
    }
}