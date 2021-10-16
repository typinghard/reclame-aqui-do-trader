using Microsoft.AspNetCore.Http;
using ReclameAquiDoTrader.Business.Interfaces.Identity;
using System;
using System.Security.Claims;

namespace ReclameAquiDoTrader.UI.Extensions
{
    public class UsuarioLogado : IUsuarioIdentity
    {
        private readonly IHttpContextAccessor _acessor;

        public UsuarioLogado(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Nome => _acessor.HttpContext.User.Identity.Name;

        public string Email => ObterUsuarioEmail();

        public bool EstaAutenticado()
        {
            return _acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string ObterUsuarioEmail()
        {
            return EstaAutenticado() ? _acessor.HttpContext.User.ObterUsuarioEmail() : string.Empty;
        }

        public string ObterUsuarioId()
        {
            return EstaAutenticado() ? _acessor.HttpContext.User.ObterUsuarioId() : string.Empty;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string ObterUsuarioId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string ObterUsuarioEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string ObterUsuarioNome(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
