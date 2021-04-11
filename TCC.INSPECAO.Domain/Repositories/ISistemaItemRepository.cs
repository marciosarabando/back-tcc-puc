using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface ISistemaItemRepository
    {
        void Criar(SistemaItem sistemaItem);
        SistemaItem ObterPorId(Guid idItemSistema);
        int ObterUltimaOrdem(Guid idSistema);
        List<SistemaItem> ObterItensSistemaInspecao(Guid idSistema);
        void Atualizar(SistemaItem sistemaItem);
    }
}