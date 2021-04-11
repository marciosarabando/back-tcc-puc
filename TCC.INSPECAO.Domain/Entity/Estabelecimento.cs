using System.Collections.Generic;

namespace TCC.INSPECAO.Domain.Entity
{
    public class Estabelecimento : Entity
    {
        public Estabelecimento()
        {
        }

        public Estabelecimento(string nome, string cnpj)
        {
            Nome = nome;
            CNPJ = cnpj;
        }
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Inspecao> Inspecoes { get; set; }
    }
}