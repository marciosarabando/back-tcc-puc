using System;
using System.Collections.Generic;

namespace TCC.INSPECAO.Domain.Models.Response
{
    public class SistemaDetalheResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int NumeroOrdem { get; set; }
        public bool Ativo { get; set; }
        public List<ItensSistemaResponse> ItensSistema { get; set; }
    }
}