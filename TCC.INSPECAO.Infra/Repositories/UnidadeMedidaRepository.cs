using System;
using System.Collections.Generic;
using System.Linq;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class UnidadeMedidaRepository : IUnidadeMedidaRepository
    {
        private readonly DataContext _context;
        public UnidadeMedidaRepository(DataContext context)
        {
            _context = context;
        }

        public List<UnidadeMedida> ObterTodos()
        {
            return _context.UnidadeMedida.OrderBy(x => x.Nome).ToList();
        }

        public UnidadeMedida ObterPorId(Guid id)
        {
            return _context.UnidadeMedida.FirstOrDefault(x => x.Id == id);
        }
    }
}