using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IInspecaoItemRepository
    {
        List<InspecaoItem> ObterItensInspecao(Guid idInspecao, Guid idSistema);
        InspecaoItem ObterItemInspecao(Guid idInspecao, Guid idItemSistema);
        void CriarItemInspecao(InspecaoItem inspecaoItem);
        void AtualizarItemInspecao(InspecaoItem inspecaoItem);
    }
}