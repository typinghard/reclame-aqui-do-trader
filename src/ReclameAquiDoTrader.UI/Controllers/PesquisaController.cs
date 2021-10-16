using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.UI.Extensions;
using ReclameAquiDoTrader.UI.ViewModels.PesquisaViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Controllers
{
    [AllowAnonymous]
    public class PesquisaController : MainController
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IMentorRepository _mentorRepository;
        private readonly IAssinanteRepository _assinanteRepository;

        public PesquisaController(
                                IAssinanteRepository assinanteRepository,
                                IAvaliacaoRepository avaliacaoRepository,
                                IMentorRepository mentorRepository,
                                INotificador notificador) : base(notificador)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _mentorRepository = mentorRepository;
            _assinanteRepository = assinanteRepository;
        }

        public IActionResult Index()
        {
            var avaliacoes = _avaliacaoRepository.Listar().Where(x => x.Inativo == false);
            var mentores = _mentorRepository.Listar();
            var viewModel = new AvaliacaoParaPesquisaViewModel();
            foreach (var avaliacao in avaliacoes)
            {
                var areasAtuacao = new List<string>();

                var mentor = mentores.FirstOrDefault(m => m.Id == avaliacao.MentorId);
                if (mentor == null)
                    continue;

                if (mentor.AreasDeAtuacao != null)
                    areasAtuacao = mentor.AreasDeAtuacao.ToList();

                viewModel.Avaliacoes.Add(new AvaliacaoParaPesquisaItemViewModel
                {
                    AvaliacaoId = avaliacao.Id,
                    Positivo = avaliacao.Positivo,
                    Respondido = avaliacao.Respondido,
                    CriadoAs = avaliacao.CriadoAs,
                    Texto = avaliacao.Texto,
                    RedesSociais = avaliacao.RedesSociais?.Lista?.Select(r =>
                        new RedeSocialViewModel()
                        {
                            Tipo = r.Tipo,
                            Url = r.Url,
                            Usuario = r.Usuario
                        }).ToList(),
                    Mentor = new MentorViewModel()
                    {
                        AreasDeAtuacao = areasAtuacao,
                        MentorId = mentor.Id,
                        Nome = mentor.Nome,
                        RedesSociais = mentor.RedesSociais?.Lista?.Select(r =>
                        new RedeSocialViewModel()
                        {
                            Tipo = r.Tipo,
                            Url = r.Url,
                            Usuario = r.Usuario
                        }).ToList(),
                    }
                });
            }
            return View(viewModel);
        }

        public IActionResult Newsletter()
        {
            return PartialView("_ModalNewsletter", new AssinarNewsletterViewModel());
        }

        public IActionResult EntendaAvalicoesNegativas()
        {
            return PartialView("_ModalEntendaAvaliacoesNegativas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssinarNewsletter([FromBody] AssinarNewsletterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            try
            {
                var assinante = new Assinante(viewModel.Whatsapp.SomenteNumeros());
                _assinanteRepository.Adicionar(assinante);
                _assinanteRepository.SalvarAlteracoes();
                Response.Cookies.Append("Assinante", "1");
            }
            catch (DomainException dex)
            {
                NotificarErro("", dex.Message);
            }
            catch (Exception)
            {
                NotificarErro("", "Desculpe, tivemos um problema. Por favor, tente novamente em alguns instantes");
            }
            return CustomResponse();
        }
    }
}
