using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IInspecaoStatusRepository
    {
        InspecaoStatus ObterPorNome(string nome);
        IEnumerable<InspecaoStatus> ObterTodos();
    }
}