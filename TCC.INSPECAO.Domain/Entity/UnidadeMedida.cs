using System.Collections.Generic;
using TCC.INSPECAO.Domain.Enums;

namespace TCC.INSPECAO.Domain.Entity
{
    public class UnidadeMedida : Entity
    {
        public UnidadeMedida()
        {

        }

        public UnidadeMedida(string nome, TipoDado tipoDado)
        {
            Nome = nome;
            TipoDado = tipoDado;
        }
        public string Nome { get; private set; }
        public virtual TipoDado TipoDado { get; private set; }
    }
}