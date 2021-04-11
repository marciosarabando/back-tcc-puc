using System;
using System.Collections.Generic;

namespace TCC.INSPECAO.Domain.Entity
{
    public class Inspecao : Entity
    {
        public Inspecao()
        {

        }

        public Inspecao(Estabelecimento estabelecimento, DateTime dataHoraInicio, DateTime? dataHoraFim, string observacao, Turno turno, Usuario usuario, InspecaoStatus inspecaoStatus)
        {
            Estabelecimento = estabelecimento;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Observacao = observacao;
            Turno = turno;
            Usuario = usuario;
            InspecaoStatus = inspecaoStatus;
        }

        public virtual Estabelecimento Estabelecimento { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime? DataHoraFim { get; private set; }
        public string Observacao { get; private set; }
        public virtual Turno Turno { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual InspecaoStatus InspecaoStatus { get; private set; }
        public virtual List<InspecaoItem> InspecaoItem { get; private set; }

        public void AlterarStatus(InspecaoStatus inspecaoStatus)
        {
            InspecaoStatus = inspecaoStatus;
        }

        public void FinalizarInspecao(string observacao)
        {
            Observacao = observacao;
            DataHoraFim = DateTime.Now;
        }
    }
}