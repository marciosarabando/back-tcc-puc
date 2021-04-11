namespace TCC.INSPECAO.Domain.Entity
{
    public class InspecaoStatus : Entity
    {
        public InspecaoStatus()
        {

        }
        public InspecaoStatus(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
    }
}