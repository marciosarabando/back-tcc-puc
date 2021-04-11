using System;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class InspecaoResponse
    {
        public Guid Id { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
    }
}