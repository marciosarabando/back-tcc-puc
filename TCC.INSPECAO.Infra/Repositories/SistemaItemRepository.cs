using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class SistemaItemRepository : ISistemaItemRepository
    {
        private readonly DataContext _context;
        public SistemaItemRepository(DataContext context)
        {
            _context = context;
        }

        public void Criar(SistemaItem sistemaItem)
        {
            _context.SistemaItem.Add(sistemaItem);
            _context.SaveChanges();
        }

        public int ObterUltimaOrdem(Guid idSistema)
        {
            var ultimaOrdem = _context.SistemaItem.Where(x => x.Sistema.Id == idSistema).Select(x => x.NumeroOrdem);

            if (ultimaOrdem.Any())
            {
                return _context.SistemaItem.Where(x => x.Sistema.Id == idSistema).Max(x => x.NumeroOrdem);
            }

            return 0;
        }

        public SistemaItem ObterPorId(Guid idItemSistema)
        {
            return _context.SistemaItem.FirstOrDefault(x => x.Id == idItemSistema);
        }

        public List<SistemaItem> ObterItensSistemaInspecao(Guid idSistema)
        {
            return _context.SistemaItem
                        .Include(x => x.UnidadeMedida)
                        .Include(x => x.Sistema)
                        .Where(x => x.Sistema.Id == idSistema && x.Ativo == true)
                        .OrderBy(x => x.NumeroOrdem)
                    .ToList();
        }

        public void Atualizar(SistemaItem sistemaItem)
        {
            _context.SistemaItem.Update(sistemaItem);
            _context.SaveChanges();
        }
    }
}