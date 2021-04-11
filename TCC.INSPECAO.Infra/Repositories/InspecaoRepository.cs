using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class InspecaoRepository : IInspecaoRepository
    {
        private readonly DataContext _context;
        public InspecaoRepository(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void Criar(Inspecao inspecao)
        {
            _context.Inspecao.Add(inspecao);
            _context.SaveChanges();
        }

        public void Atualizar(Inspecao inspecao)
        {
            _context.Inspecao.Update(inspecao);
            _context.SaveChanges();
        }

        public Inspecao ObterPorId(Guid id)
        {
            return _context.Inspecao.FirstOrDefault(x => x.Id == id);
        }

        public Inspecao ObterInspecaoEmAndamentoPorUsuario(Guid idUsuario)
        {
            return _context.Inspecao.OrderByDescending(x => x.DataHoraInicio)
            .FirstOrDefault(x => x.Usuario.Id == idUsuario && x.DataHoraFim == null);
        }

        public IEnumerable<Inspecao> ObterTodos()
        {
            return _context.Inspecao
                                .AsNoTracking();
        }

        public IEnumerable<Inspecao> ObterRelatorioPeriodo(DateTime inicio, DateTime fim)
        {
            return _context.Inspecao
            .Include(x => x.Usuario)
            .Include(x => x.InspecaoStatus)
            .Where(x => x.DataHoraInicio >= inicio && x.DataHoraFim <= fim)
            .OrderByDescending(x => x.DataHoraInicio)
            .ToList();
        }

        public IEnumerable<Inspecao> ObterRelatorioPorUsuario(Guid idUsuario)
        {
            return _context.Inspecao
            .Include(x => x.Usuario)
            .Include(x => x.InspecaoStatus)
            .Where(x => x.Usuario.Id == idUsuario)
            .OrderByDescending(x => x.DataHoraInicio)
            .ToList();
        }

        public Inspecao ObterRelatorioInspecaoDetalhes(Guid idInspecao)
        {
            return _context.Inspecao
            .Include(x => x.Usuario)
            .Include(x => x.InspecaoStatus)
            .Include(x => x.Turno)
            .Include(x => x.InspecaoItem)
            .ThenInclude(x => x.SistemaItem)
            .ThenInclude(x => x.UnidadeMedida)
            .Include(x => x.InspecaoItem)
            .ThenInclude(x => x.SistemaItem)
            .ThenInclude(x => x.Sistema)
            .Where(x => x.Id == idInspecao)
            .FirstOrDefault();
        }

        public List<int> ObterAnosDisponiveisInspecao()
        {
            return _context.Inspecao.Select(x => x.DataHoraInicio.Year).Distinct().ToList();
        }

        public List<TResult> ObterUsuariosDisponiveisInspecao<TResult>(Expression<Func<Inspecao, TResult>> expression)
        {
            return _context.Inspecao.Select(expression).Distinct().ToList();
        }
    }
}