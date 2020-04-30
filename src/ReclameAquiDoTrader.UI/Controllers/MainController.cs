using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

            return Json(new
            {
                erros = _notificador.ObterNotificacoes()
            });
        }

        protected void NotificarErro(string chave, string mensagem)
        {
            _notificador.Handle(new Notificacao(chave, mensagem));
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            foreach (var erro in modelState)
            {
                erro.Value.Errors
                        .ToList()
                        .ForEach(x => NotificarErro(erro.Key, x.ErrorMessage != null ? x.ErrorMessage : x.Exception.Message));
            }
        }
    }
}
