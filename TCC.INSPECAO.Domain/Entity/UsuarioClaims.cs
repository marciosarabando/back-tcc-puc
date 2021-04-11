using System;
using Newtonsoft.Json;

namespace TCC.INSPECAO.Domain.Entity
{
    public class UsuarioClaims
    {
        public Guid UsuarioId { get; set; }
        public Guid ClaimId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
        [JsonIgnore]
        public virtual Claims Claim { get; set; }
    }
}