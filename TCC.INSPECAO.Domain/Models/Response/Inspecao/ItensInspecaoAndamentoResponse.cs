using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Enums;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class ItensInspecaoAndamentoResponse
    {
        public string NomeSistema { get; set; }
        public List<ItensInspecao> ItensInspecao { get; set; }

    }

    public class ItensInspecao
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public TipoDado TipoDado { get; set; }
        public string Valor { get; set; }
    }
}