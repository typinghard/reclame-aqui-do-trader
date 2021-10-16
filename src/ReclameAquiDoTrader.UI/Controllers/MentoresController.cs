using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.Business.ValueObjects;
using ReclameAquiDoTrader.UI.Extensions;
using ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class MentoresController : MainController
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IMapper _mapper;

        public MentoresController(INotificador notificador,
                                  IMapper mapper,
                                  IMentorRepository mentorRepository) : base(notificador)
        {
            _mapper = mapper;
            _mentorRepository = mentorRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new DetalhesMentoresItemViewModels();

            var mentores = _mentorRepository.Listar().OrderByDescending(m => m.CriadoAs);
            foreach (var mentor in mentores)
            {
                var telefones = new List<TelefoneViewModel>();
                var emails = new List<EmailViewModel>();
                var redesSociais = new List<RedeSocialViewModel>();

                if (mentor.Telefones?.Telefones?.Count > 0)
                    foreach (var telefone in mentor.Telefones.Telefones)
                        telefones.Add(new TelefoneViewModel()
                        {
                            Numero = telefone.Numero
                        });

                if (mentor.RedesSociais?._redeSocial?.Count > 0)
                    foreach (var redeSocial in mentor.RedesSociais._redeSocial)
                        redesSociais.Add(new RedeSocialViewModel()
                        {
                            Tipo = redeSocial.Tipo,
                            Url = redeSocial.Url,
                            Usuario = redeSocial.Usuario
                        });

                if (mentor.Emails?._emails?.Count > 0)
                    foreach (var email in mentor.Emails._emails)
                        emails.Add(new EmailViewModel()
                        {
                            Endereco = email.Endereco
                        });

                viewModel.Mentores.Add(new DetalhesMentorViewModels()
                {
                    Id = mentor.Id,
                    CriadoAs = mentor.CriadoAs,
                    AtualizadoAs = mentor.AtualizadoAs,
                    AreasDeAtuacao = mentor.AreasDeAtuacao?.ToList(),
                    Inativo = mentor.Inativo,
                    Nome = mentor.Nome,
                    Site = mentor.Site,
                    Telefones = telefones,
                    Emails = emails,
                    RedesSociais = redesSociais
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var viewModel = new MentorItemViewModel();
            viewModel.AreasAtuacaoExistentes = _mentorRepository.Listar().ToList().AreasAtuacaoDistinct();

            return PartialView("_Cadastrar", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar([FromBody] CriarMentorViewModel viewModel)
        {
            if (viewModel == null || !ModelState.IsValid)
                return BadRequest();

            try
            {
                //   var mentor1 = _mapper.Map<Mentor>(viewModel);
            }
            catch (System.Exception ex)
            {
                var erro = ex.Message;
                //throw;
            }

            if (viewModel.RedesSociais.Count() == 0)
            {
                NotificarErro("Criar Mentor", "É necessário ter pelo menos uma rede social!");
                return CustomResponse();
            }


            var mentor = new Mentor(viewModel.Nome);
            mentor.AtribuirSite(viewModel.Site);

            foreach (var areaAtuacao in viewModel.AreasDeAtuacao)
                mentor.AdicionarAreaDeAtuacao(areaAtuacao);

            foreach (var emailVM in viewModel.Emails)
            {
                var email = new Email(emailVM.Endereco);
                mentor.Emails.Adicionar(email);
            }

            foreach (var telefoneVM in viewModel.Telefones)
            {
                var telefone = new Telefone(telefoneVM.Numero);
                mentor.Telefones.Adicionar(telefone);
            }

            foreach (var lista in viewModel.RedesSociais)
            {
                var redeSocial = new RedeSocial(lista.Url, lista.Tipo);
                redeSocial.DefinirUsuario(lista.Usuario);

                mentor.RedesSociais.Adicionar(redeSocial);
            }

            _mentorRepository.Adicionar(mentor);
            _mentorRepository.SalvarAlteracoes();

            return CustomResponse(new
            {
                url_lista_mentores = Url.Action("Index", "Mentores"),
                url_painel_principal = Url.Action("Index", "Dashboard")
            });
        }


        [HttpGet]
        public IActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return CustomResponse();

            var viewModel = RecuperarDetalhesMentor(id);
            if (viewModel == null)
            {
                NotificarErro("Editar", "Mentor não encontrado!");
                return CustomResponse();
            }

            viewModel.AreasAtuacaoExistentes = _mentorRepository.Listar().ToList().AreasAtuacaoDistinct();

            return PartialView("_Editar", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([FromBody] AlterarMentorViewModel viewModel)
        {
            if (viewModel == null || !ModelState.IsValid)
                return BadRequest();

            var mentor = _mentorRepository.ObterPorId(viewModel.Id);
            if (mentor == null)
            {
                NotificarErro("Erro", "Mentor não encontrado!");
                return CustomResponse();
            }

            mentor.AtribuirSite(viewModel.Site);
            mentor.AtualizarNome(viewModel.Nome);

            mentor.LimparAreaDeAtuacao();
            foreach (var areaAtuacao in viewModel.AreasDeAtuacao)
                mentor.AdicionarAreaDeAtuacao(areaAtuacao);

            mentor.Emails.LimparEmails();
            foreach (var emailVM in viewModel.Emails)
            {
                var email = new Email(emailVM.Endereco);
                mentor.Emails.Adicionar(email);
            }

            mentor.Telefones.LimparTelefones();
            foreach (var telefoneVM in viewModel.Telefones)
            {
                var telefone = new Telefone(telefoneVM.Numero);
                mentor.Telefones.Adicionar(telefone);
            }

            mentor.RedesSociais.LimparRedesSociais();
            foreach (var lista in viewModel.RedesSociais)
            {
                var redeSocial = new RedeSocial(lista.Url, lista.Tipo);
                redeSocial.DefinirUsuario(lista.Usuario);

                mentor.RedesSociais.Adicionar(redeSocial);
            }

            _mentorRepository.Atualizar(mentor);
            _mentorRepository.SalvarAlteracoes();

            return CustomResponse(new
            {
                url_detalhes = Url.Action("Detalhes", "Mentores", new { id = mentor.Id }),
                url_lista_mentores = Url.Action("Index", "Mentores"),
                url_painel_principal = Url.Action("Index", "Dashboard")
            });
        }

        [HttpGet]
        public IActionResult Detalhes(string id)
        {
            if (string.IsNullOrEmpty(id))
                return CustomResponse();

            var viewModel = RecuperarDetalhesMentor(id);
            if (viewModel == null)
            {
                NotificarErro("Detalhes", "Mentor não encontrado!");
                return CustomResponse();
            }

            return PartialView("_Detalhes", viewModel);
        }

        [HttpGet]
        public IActionResult Inativar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return CustomResponse();

            var viewModel = RecuperarDetalhesMentor(id);
            if (viewModel == null)
            {
                NotificarErro("Detalhes", "Mentor não encontrado!");
                return CustomResponse();
            }

            return PartialView("_Inativar", viewModel);
        }

        [HttpPost, ActionName("Inativar")]
        public IActionResult InativarConfirmado([FromBody] RemoverMentorViewModel viewModel)
        {
            var mentor = _mentorRepository.ObterPorId(viewModel.Id);

            if (mentor == null)
                return NotFound();

            _mentorRepository.Inativar(mentor);
            _mentorRepository.SalvarAlteracoes();

            return CustomResponse(new
            {
                url = Url.Action("Inativos", "Mentores"),
                url_mentores = Url.Action("Index", "Mentores"),
            });
        }

        [HttpGet]
        public IActionResult Ativar(AtivarMentorViewModel viewModel)
        {
            var mentor = _mentorRepository.ObterPorId(viewModel.Id);

            if (mentor == null)
                return NotFound();

            _mentorRepository.Ativar(mentor);
            _mentorRepository.SalvarAlteracoes();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult QuantidadeMentores()
        {
            var viewModel = new DetalhesMentoresItemViewModels();
            var mentores = _mentorRepository.Listar();

            viewModel.QtdeMentores = new QuantidadeMentoresViewModel()
            {
                Qtde = mentores.Count(),
                QtdeReferenteMesPassado = 0,
                Texto = ""
            };

            return PartialView("~/Views/Dashboard/_QtdeMentoresCadastrados.cshtml", viewModel);
        }


        public DetalhesMentorViewModels RecuperarDetalhesMentor(string id)
        {
            var viewModel = new DetalhesMentorViewModels();

            var telefones = new List<TelefoneViewModel>();
            var emails = new List<EmailViewModel>();
            var redesSociais = new List<RedeSocialViewModel>();

            var mentor = _mentorRepository.ObterPorId(id);

            if (mentor.Telefones?.Telefones != null && mentor.Telefones.Telefones.Count > 0)
                foreach (var telefone in mentor.Telefones.Telefones)
                    telefones.Add(new TelefoneViewModel()
                    {
                        Numero = telefone.Numero
                    });

            if (mentor.RedesSociais?._redeSocial != null && mentor.RedesSociais._redeSocial.Count > 0)
                foreach (var redeSocial in mentor.RedesSociais._redeSocial)
                    redesSociais.Add(new RedeSocialViewModel()
                    {
                        Tipo = redeSocial.Tipo,
                        Url = redeSocial.Url,
                        Usuario = redeSocial.Usuario
                    });

            if (mentor.Emails?._emails != null && mentor.Emails._emails.Count > 0)
                foreach (var email in mentor.Emails._emails)
                    emails.Add(new EmailViewModel()
                    {
                        Endereco = email.Endereco
                    });

            viewModel = new DetalhesMentorViewModels()
            {
                Id = mentor.Id,
                CriadoAs = mentor.CriadoAs,
                AtualizadoAs = mentor.AtualizadoAs,
                AreasDeAtuacao = mentor.AreasDeAtuacao?.ToList(),
                Inativo = mentor.Inativo,
                Nome = mentor.Nome,
                Site = mentor.Site,
                Telefones = telefones,
                Emails = emails,
                RedesSociais = redesSociais,
            };

            return viewModel;
        }
    }
}
