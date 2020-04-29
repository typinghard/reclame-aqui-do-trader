using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class AvaliacoesController : MainController
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        public AvaliacoesController(IAvaliacaoRepository avaliacaoRepository,
                                    INotificador notificador) : base(notificador)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
