using System;

namespace TCC.INSPECAO.Domain.Entity
{
    public class InspecaoItem : Entity
    {
        public InspecaoItem()
        {

        }

        public InspecaoItem(Inspecao inspecao, DateTime dataHora, string observacao, SistemaItem sistemaItem, string valor)
        {
            Inspecao = inspecao;
            DataHora = dataHora;
            Observacao = observacao;
            SistemaItem = sistemaItem;
            Valor = valor;
        }


        public Inspecao Inspecao { get; private set; }
        public DateTime DataHora { get; private set; }
        public string Observacao { get; private set; }
        public string Valor { get; private set; }
        public virtual SistemaItem SistemaItem { get; private set; }

        public void AtualizaValor(string valor)
        {
            Valor = valor;
            DataHora = DateTime.Now;
        }
    }
}