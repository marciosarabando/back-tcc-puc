using System.Collections.Generic;

namespace TCC.INSPECAO.Domain.Entity
{
    public class SistemaItem : Entity
    {
        public SistemaItem()
        {

        }

        public SistemaItem(string nome, string descricao, Sistema sistema, int numeroOrdem, UnidadeMedida unidadeMedida)
        {
            Nome = nome;
            Descricao = descricao;
            Sistema = sistema;
            NumeroOrdem = numeroOrdem;
            UnidadeMedida = unidadeMedida;
            Ativo = true;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int NumeroOrdem { get; private set; }
        public virtual UnidadeMedida UnidadeMedida { get; private set; }
        public virtual Sistema Sistema { get; private set; }
        public virtual List<InspecaoItem> InspecaoItens { get; private set; }
        public bool Ativo { get; set; }

        public void AlterarSistemaItem(string nome, string descricao, UnidadeMedida unidadeMedida)
        {
            Nome = nome;
            Descricao = descricao;
            UnidadeMedida = unidadeMedida;
        }

        public void AtivarDesativar(bool ativo)
        {
            Ativo = ativo;
        }
        public void AtualizarOrdem(int numeroOrdem)
        {
            NumeroOrdem = numeroOrdem;
        }
    }
}