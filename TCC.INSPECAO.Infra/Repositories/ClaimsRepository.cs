using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly DataContext _context;
        public ClaimsRepository(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public Claims ObterPorNomeValor(string nome, string valor)
        {
            return _context.Claims.FirstOrDefault(x => x.Nome == nome && x.Valor == valor);
        }

        public IEnumerable<Claims> ObterTodos()
        {
            return _context.Claims
                                .AsNoTracking();
        }
    }
}