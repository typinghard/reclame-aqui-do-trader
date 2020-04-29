using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class MasterController : MainController
    {
        public MasterController(INotificador notificador) : base(notificador)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
