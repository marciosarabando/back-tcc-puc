using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IClaimsRepository
    {
        IEnumerable<Claims> ObterTodos();
        Claims ObterPorNomeValor(string nome, string valor);
    }
}