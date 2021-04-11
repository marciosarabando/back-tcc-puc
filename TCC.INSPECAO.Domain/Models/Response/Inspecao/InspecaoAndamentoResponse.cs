using System;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class InspecaoAndamentoResponse
    {
        public bool PossuiInspecaoAndamento { get; set; }
        public Guid? idInspecao { get; set; }
        public DateTime? DataHoraInicio { get; set; }
    }
}