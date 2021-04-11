using System;
using System.Linq;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly DataContext _context;
        public TurnoRepository(DataContext context)
        {
            _context = context;
        }

        public Turno ObterPorSigla(string sigla)
        {
            return _context.Turno.Where(x => x.Sigla == sigla).FirstOrDefault();
        }

        public Turno ObterPorHorario(DateTime horaAtual)
        {
            return _context.Turno
                .Where(x => horaAtual.TimeOfDay >= x.HoraInicio.TimeOfDay && horaAtual.TimeOfDay <= x.HoraFim.TimeOfDay)
                .FirstOrDefault();
        }
    }
}