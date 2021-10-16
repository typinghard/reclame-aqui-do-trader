using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;

namespace ReclameAquiDoTrader.UI.Controllers
{
    [AllowAnonymous]
    public class PromosController : MainController
    {
        public PromosController(
            INotificador notificador) : base(notificador) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
