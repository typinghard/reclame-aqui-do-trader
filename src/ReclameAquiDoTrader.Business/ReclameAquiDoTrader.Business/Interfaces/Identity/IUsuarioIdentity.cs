using System.Collections.Generic;
using System.Security.Claims;

namespace ReclameAquiDoTrader.Business.Interfaces.Identity
{
    public interface IUsuarioIdentity
    {
        string Nome { get; }
        string Email { get; }
        string OberUsuarioId();
        string OberUsuarioEmail();
        bool EstaAutenticado();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
    }
}
