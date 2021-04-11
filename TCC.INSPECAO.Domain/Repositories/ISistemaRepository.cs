using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface ISistemaRepository
    {
        Sistema ObterPorId(Guid id);
        Sistema ObterDetalhesPorId(Guid id);
        int ObterUltimaOrdem();
        List<Sistema> ObterTodos();
        List<Sistema> ObterSistemasPorEstabelecimento(Guid idEstabelecimento);
        void Criar(Sistema sistema);
        void Atualizar(Sistema sistema);
    }
}