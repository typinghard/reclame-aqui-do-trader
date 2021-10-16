namespace ReclameAquiDoTrader.Business.Interfaces.Identity
{
    public interface IUsuarioIdentity
    {
        string Nome { get; }
        string Email { get; }
        string ObterUsuarioId();
        string ObterUsuarioEmail();
        bool EstaAutenticado();
    }
}
