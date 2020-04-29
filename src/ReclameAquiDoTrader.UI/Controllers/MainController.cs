using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Identity;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        private readonly INotificador _notificador;
        public readonly IUsuarioIdentity UsuarioLogado;
        protected MainController(IUsuarioIdentity usuarioIdentity,
                                 INotificador notificador)
        {
            UsuarioLogado = usuarioIdentity;
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected IActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                erro = string.Join(", ", _notificador.ObterNotificacoes().Select(n => n.Mensagem))
            });
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
