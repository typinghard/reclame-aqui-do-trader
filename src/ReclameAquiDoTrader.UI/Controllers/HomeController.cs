using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Identity;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class HomeController : MainController
    {
        public HomeController(INotificador notificador,
                              IUsuarioIdentity usuarioIdentity) : base(usuarioIdentity, notificador)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
