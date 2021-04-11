using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class SistemaRepository : ISistemaRepository
    {
        private readonly DataContext _context;
        public SistemaRepository(DataContext context)
        {
            _context = context;
        }

        public Sistema ObterPorId(Guid id)
        {
            return _context.Sistema.FirstOrDefault(x => x.Id == id);
        }


        public Sistema ObterDetalhesPorId(Guid id)
        {
            return _context.Sistema
                .Include(x => x.SistemaItens)
                .ThenInclude(x => x.UnidadeMedida)
                .FirstOrDefault(x => x.Id == id);
        }

        public int ObterUltimaOrdem()
        {
            var ultimaOrdem = _context.Sistema.Select(x => x.NumeroOrdem);

            if (ultimaOrdem.Any())
            {
                return _context.Sistema.Max(x => x.NumeroOrdem);
            }

            return 0;
        }

        public List<Sistema> ObterTodos()
        {
            return _context.Sistema.ToList();
        }

        public void Criar(Sistema sistema)
        {
            _context.Sistema.Add(sistema);
            _context.SaveChanges();

        }

        public List<Sistema> ObterSistemasPorEstabelecimento(Guid idEstabelecimento)
        {
            return _context.Sistema
                    .Include(x => x.SistemaItens)
                    .Where(x => x.Estabelecimento.Id == idEstabelecimento).OrderBy(x => x.NumeroOrdem)
                    .ToList();
        }

        public void Atualizar(Sistema sistema)
        {
            _context.Sistema.Update(sistema);
            _context.SaveChanges();
        }
    }
}