using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Domain.Entity;

namespace DevIO.Api.Extensions
{
    public class CustomAuthorization
    {

        public static bool ValidarClaimsUsuario(Usuario user, string claimName, string claimValue)
        {
            if (claimValue.Contains(","))
            {
                var claimsValue = claimValue.Split(',');

                foreach (var claim in claimsValue)
                {
                    if (user.UsuarioClaims.Select(x => x.Claim).ToList().Any(c => c.Nome == claimName && c.Valor.Contains(claim.Trim())))
                        return true;
                }
                return false;

            }
            //return user.UsuarioClaims.Any(c => c.Claim.Nome == claimName && c.Claim.Valor.Contains(claimValue));
            return user.UsuarioClaims.Select(x => x.Claim).ToList().Any(c => c.Nome == claimName && c.Valor.Contains(claimValue));

        }

    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        private IUsuarioRepository _usuarioRepository;

        public RequisitoClaimFilter(Claim claim, IUsuarioRepository usuarioRepository)
        {
            _claim = claim;
            _usuarioRepository = usuarioRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var idUserFirebase = context.HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type.Contains("user_id")).Value;

            var user = _usuarioRepository.ObterPorIdFirebase(idUserFirebase);

            if (user is null)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!CustomAuthorization.ValidarClaimsUsuario(user, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}