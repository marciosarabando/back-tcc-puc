using System.Collections.Generic;
using Newtonsoft.Json;

namespace TCC.INSPECAO.Domain.Entity
{
    public class Claims : Entity
    {
        public Claims() { }

        public Claims(string nome, string valor)
        {
            Nome = nome;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Valor { get; private set; }

        [JsonIgnore]
        public virtual List<UsuarioClaims> UsuarioClaims { get; set; }
    }
}