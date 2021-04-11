using System;
using System.Collections.Generic;

namespace TCC.INSPECAO.Domain.Models.Response.Relatorio
{
    public class RelatorioInspecaoDetalhesResponse
    {
        public Guid IdInspecao { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public string Status { get; set; }
        public string Turno { get; set; }
        public string Observacao { get; set; }
        public string Usuario { get; set; }
        public List<SistemaInspecaoResponse> SistemaInspecaoResponse { get; set; }

    }

    public class SistemaInspecaoResponse
    {
        public Guid IdSistema { get; set; }
        public string NomeSistema { get; set; }
        public List<ItensSistemaInspecaoResponse> ItensInspecao { get; set; }
    }

    public class ItensSistemaInspecaoResponse
    {
        public DateTime DataHora { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public string Valor { get; set; }
    }
}