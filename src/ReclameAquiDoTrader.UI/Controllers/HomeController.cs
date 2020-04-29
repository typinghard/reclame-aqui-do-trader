using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class HomeController : MainController
    {
        public HomeController(INotificador notificador) : base(notificador)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
