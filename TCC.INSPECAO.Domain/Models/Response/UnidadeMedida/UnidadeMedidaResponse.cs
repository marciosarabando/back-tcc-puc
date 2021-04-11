using System;
using TCC.INSPECAO.Domain.Enums;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class UnidadeMedidaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public TipoDado TipoDado { get; set; }
    }
}