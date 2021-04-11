using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IEstabelecimentoRepository
    {
        List<Estabelecimento> ObterTodos();
        Estabelecimento ObterPorCNPJ(string cnpj);
        Estabelecimento ObterPorId(Guid id);
    }
}