using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class InspecaoStatusRepository : IInspecaoStatusRepository
    {
        private readonly DataContext _context;
        public InspecaoStatusRepository(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public InspecaoStatus ObterPorNome(string nome)
        {
            return _context.InspecaoStatus.FirstOrDefault(x => x.Nome == nome);
        }

        IEnumerable<InspecaoStatus> IInspecaoStatusRepository.ObterTodos()
        {
            return _context.InspecaoStatus.AsNoTracking();
        }
    }
}