using System;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class SistemasRondaResponse
    {
        public Guid IdSistema { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public bool InspecaoConcluida { get; set; }
    }
}