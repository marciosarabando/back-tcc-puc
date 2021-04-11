using System;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface ITurnoRepository
    {
        Turno ObterPorSigla(string sigla);
        Turno ObterPorHorario(DateTime horaAtual);
    }
}