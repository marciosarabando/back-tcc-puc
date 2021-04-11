using System;
using TCC.INSPECAO.Domain.Enums;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class ItensSistemaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int NumeroOrdem { get; set; }
        public Guid IdUnidadeMedida { get; set; }
        public string NomeUnidadeMedida { get; set; }
        public TipoDado TipoDado { get; set; }
        public bool Ativo { get; set; }
    }
}