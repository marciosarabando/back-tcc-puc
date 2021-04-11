using System.Collections.Generic;

namespace TCC.INSPECAO.Domain.Entity
{
    public class Sistema : Entity
    {
        public Sistema()
        {
        }

        public Sistema(string nome, string descricao, int ordem)
        {
            Nome = nome;
            Descricao = descricao;
            NumeroOrdem = ordem;
        }

        public Sistema(string nome, string descricao, int ordem, Estabelecimento estabelecimento)
        {
            Nome = nome;
            Descricao = descricao;
            NumeroOrdem = ordem;
            Estabelecimento = estabelecimento;
            Ativo = true;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int NumeroOrdem { get; private set; }
        public virtual Estabelecimento Estabelecimento { get; private set; }
        public virtual List<SistemaItem> SistemaItens { get; private set; }
        public bool Ativo { get; set; }

        public void AlterarNomeDescricao(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
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