using System;

namespace TCC.INSPECAO.Domain.Entity
{
    public class Turno : Entity
    {
        public Turno()
        {

        }
        public Turno(string nome, string sigla, DateTime horaInicio, DateTime horaFim)
        {
            Nome = nome;
            Sigla = sigla;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public DateTime HoraInicio { get; private set; }
        public DateTime HoraFim { get; private set; }
    }
}