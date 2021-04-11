using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class InspecaoItemRepository : IInspecaoItemRepository
    {

        private readonly DataContext _context;
        public InspecaoItemRepository(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public List<InspecaoItem> ObterItensInspecao(Guid idInspecao, Guid idSistema)
        {
            //return _context.InspecaoItem.Where(x => x.Inspecao.Id == idInspecao).ToList();

            return (from t1 in _context.InspecaoItem
                    join t2 in _context.SistemaItem on t1.SistemaItem.Id equals t2.Id
                    where (t1.Inspecao.Id == idInspecao)
                    && (t2.Sistema.Id == idSistema)
                    select new InspecaoItem(t1.Inspecao, t1.DataHora, t1.Observacao, t1.SistemaItem, t1.Valor) { }
            ).ToList();
        }

        public InspecaoItem ObterItemInspecao(Guid idInspecao, Guid idItemSistema)
        {
            return _context.InspecaoItem
                    .Include(x => x.SistemaItem).ThenInclude(x => x.Sistema)
                    .Where(x => x.Inspecao.Id == idInspecao && x.SistemaItem.Id == idItemSistema)
                    .FirstOrDefault();
        }

        public void CriarItemInspecao(InspecaoItem inspecaoItem)
        {
            _context.InspecaoItem.Add(inspecaoItem);
            _context.SaveChanges();
        }

        public void AtualizarItemInspecao(InspecaoItem inspecaoItem)
        {
            _context.InspecaoItem.Update(inspecaoItem);
            _context.SaveChanges();
        }
    }
}