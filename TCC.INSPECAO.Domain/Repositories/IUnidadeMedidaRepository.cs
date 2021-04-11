using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IUnidadeMedidaRepository
    {
        List<UnidadeMedida> ObterTodos();
        UnidadeMedida ObterPorId(Guid id);
    }
}