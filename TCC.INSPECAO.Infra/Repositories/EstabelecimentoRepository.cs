using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private readonly DataContext _context;
        public EstabelecimentoRepository(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public Estabelecimento ObterPorCNPJ(string cnpj)
        {
            return _context.Estabelecimento.FirstOrDefault(x => x.CNPJ == cnpj);
        }

        public List<Estabelecimento> ObterTodos()
        {
            return _context.Estabelecimento
                                .AsNoTracking().ToList();
        }

        public Estabelecimento ObterPorId(Guid id)
        {
            return _context.Estabelecimento
                                .FirstOrDefault(x => x.Id == id);
        }
    }
}