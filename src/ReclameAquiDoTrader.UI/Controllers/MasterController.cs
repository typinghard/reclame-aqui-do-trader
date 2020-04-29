using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Identity;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class MasterController : MainController
    {
        public MasterController(INotificador notificador,
                                IUsuarioIdentity usuarioIdentity) : base(usuarioIdentity, notificador)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}