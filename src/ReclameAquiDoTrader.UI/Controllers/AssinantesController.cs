using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.UI.ViewModels.AssinantesViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class AssinantesController : MainController
    {
        private readonly IAssinanteRepository _assinanteRepository;

        public AssinantesController(IAssinanteRepository assinanteRepository,
                                    INotificador notificador) : base(notificador)
        {
            _assinanteRepository = assinanteRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new List<DetalhesAssinantesViewModel>();

            var assinantes = _assinanteRepository.Listar().OrderByDescending(m => m.CriadoAs);
            foreach (var assinante in assinantes)
                viewModel.Add(new DetalhesAssinantesViewModel()
                {
                    CriadoAs = assinante.CriadoAs,
                    Id = assinante.Id,
                    Telefone = assinante.Whatsapp
                });

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Detalhes(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var viewModel = new DetalhesAssinantesViewModel();

            var assinante = _assinanteRepository.ObterPorId(id);
            if (assinante == null)
            {
                NotificarErro("Detalhes", "Assinante não encontrado");
                return CustomResponse();
            }

            viewModel.Id = assinante.Id;
            viewModel.CriadoAs = assinante.CriadoAs;
            viewModel.Telefone = assinante.Whatsapp;

            return PartialView("_Detalhes", viewModel);
        }
    }
}
