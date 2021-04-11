using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IUsuarioClaimsRepository
    {
        void Criar(UsuarioClaims usuarioClaims);
        void Atualizar(UsuarioClaims usuarioClaims);
        void Remover(UsuarioClaims usuarioClaims);
        List<UsuarioClaims> ObterClaimsUsuario(string idFirebase);
    }
}