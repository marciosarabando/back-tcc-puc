using System;
using System.Linq.Expressions;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Queries
{
    public static class UsuarioQueries
    {
        public static Expression<Func<Usuario, bool>> ObterPorEmail(string email)
        {
            return x => x.Email == email;
        }
    }
}