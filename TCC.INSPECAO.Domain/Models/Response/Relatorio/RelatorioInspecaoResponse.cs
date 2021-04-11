using System;

namespace TCC.INSPECAO.Domain.Models.Response.Relatorio
{
    public class RelatorioInspecaoResponse
    {
        public Guid IdInspecao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Status { get; set; }
        public string Usuario { get; set; }
    }
}