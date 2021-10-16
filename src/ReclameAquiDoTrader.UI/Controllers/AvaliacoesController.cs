using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Enums;
using ReclameAquiDoTrader.Business.Interfaces.Data;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.Business.ValueObjects;
using ReclameAquiDoTrader.UI.Extensions;
using ReclameAquiDoTrader.UI.Interfaces;
using ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels;
using ReclameAquiDoTrader.UI.ViewModels.PublicacaoViewModel;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class AvaliacoesController : MainController
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IMentorRepository _mentorRepository;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPublicacaoService _publicacaoService;
        public AvaliacoesController(IAvaliacaoRepository avaliacaoRepository,
                                    IMentorRepository mentorRepository,
                                    IMapper mapper,
                                    IAzureStorageService azureStorageService,
                                    IWebHostEnvironment env,
                                    IPublicacaoService publicacaoService,
                                    INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _avaliacaoRepository = avaliacaoRepository;
            _mentorRepository = mentorRepository;
            _azureStorageService = azureStorageService;
            _webHostEnvironment = env;
            _publicacaoService = publicacaoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new DetalhesAvaliacaoItemViewModel();
            var avaliacoes = _avaliacaoRepository.Listar();
            var mentores = _mentorRepository.Listar();

            foreach (var avaliacao in avaliacoes)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                var comprovantes = ListarComprovantes(avaliacao);

                viewModel.Avaliacoes.Add(new DetalhesAvaliacaoViewModel()
                {
                    Id = avaliacao.Id,
                    AtualizadoAs = avaliacao.AtualizadoAs,
                    CriadoAs = avaliacao.CriadoAs,
                    DataExpiracao = avaliacao.DataExpiracao,
                    Expirado = avaliacao.Expirado,
                    DataResposta = avaliacao.DataResposta,
                    Respondido = avaliacao.Respondido,
                    HorasParaResposta = avaliacao.HorasParaResposta,
                    Inativo = avaliacao.Inativo,
                    Positivo = avaliacao.Positivo,
                    RedesSociais = avaliacao.RedesSociais,
                    Texto = avaliacao.Texto,
                    Mentor = mentor.Nome,
                    Comprovantes = comprovantes
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult PendentesPublicacao()
        {
            var viewModel = new List<AvaliacoesPendentesPublicacaoViewModel>();

            var avaliacoes = _avaliacaoRepository.Listar().ToList().AvaliacoesPendentesDePublicacao();
            var mentores = _mentorRepository.Listar();

            foreach (var avaliacao in avaliacoes)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                viewModel.Add(new AvaliacoesPendentesPublicacaoViewModel()
                {
                    Id = avaliacao.Id,
                    CriadoAs = avaliacao.CriadoAs,
                    AtualizadoAs = avaliacao.AtualizadoAs,
                    DataExpiracao = avaliacao.DataExpiracao,
                    Expirado = avaliacao.Expirado,
                    Mentor = mentor.Nome,
                    RedesSociais = avaliacao.RedesSociais,
                    Respondido = avaliacao.Respondido,
                    Positivo = avaliacao.Positivo,
                    Publicado = avaliacao.Publicado,
                    Texto = string.IsNullOrEmpty(avaliacao.Texto) ? false : true
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EmAndamento()
        {
            var viewModel = new List<AvaliacoesEmAndamentoViewModel>();

            var avaliacoes = _avaliacaoRepository.Listar().ToList().AvaliacoesEmAndamento();
            var mentores = _mentorRepository.Listar();

            foreach (var avaliacao in avaliacoes)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                viewModel.Add(new AvaliacoesEmAndamentoViewModel()
                {
                    Id = avaliacao.Id,
                    CriadoAs = avaliacao.CriadoAs,
                    AtualizadoAs = avaliacao.AtualizadoAs,
                    DataExpiracao = avaliacao.DataExpiracao,
                    Expirado = avaliacao.Expirado,
                    Mentor = mentor.Nome,
                    RedesSociais = avaliacao.RedesSociais,
                    Respondido = avaliacao.Respondido,
                    Positivo = avaliacao.Positivo,
                    Publicado = avaliacao.Publicado,
                    Texto = string.IsNullOrEmpty(avaliacao.Texto) ? false : true
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Publicadas()
        {
            var viewModel = new List<AvaliacoesPublicadasViewModel>();

            var avaliacoes = _avaliacaoRepository.Listar().ToList().AvaliacoesPublicadas();
            var mentores = _mentorRepository.Listar();

            foreach (var avaliacao in avaliacoes)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                viewModel.Add(new AvaliacoesPublicadasViewModel()
                {
                    Id = avaliacao.Id,
                    PublicadoAs = avaliacao.DataPublicacao.GetValueOrDefault(),
                    Publicado = avaliacao.Publicado,
                    AtualizadoAs = avaliacao.AtualizadoAs,
                    DataExpiracao = avaliacao.DataExpiracao,
                    Mentor = mentor.Nome,
                    RedesSociais = avaliacao.RedesSociais,
                    Respondido = avaliacao.Respondido,
                    Positivo = avaliacao.Positivo,
                    Expirado = avaliacao.Expirado,
                    Texto = string.IsNullOrEmpty(avaliacao.Texto) ? false : true
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var mentores = _mentorRepository.Listar();
            var viewModel = new CriarAvaliacaoViewModel();

            foreach (var mentor in mentores)
                viewModel.Mentores.Add(new MentorViewModel()
                {
                    Id = mentor.Id,
                    Nome = mentor.Nome
                });


            return PartialView("_Cadastrar", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar([FromForm] CriarAvaliacaoViewModel viewModel)
        {
            if (viewModel == null || !ModelState.IsValid)
                return BadRequest();

            //var avaliacao = _mapper.Map<Avaliacao>(viewModel);

            var avaliacao = new Avaliacao(viewModel.MentorId, viewModel.Texto);

            if (viewModel.Positivo)
                avaliacao.Positivar();

            if (!string.IsNullOrEmpty(viewModel.Resposta))
                avaliacao.Responder(viewModel.Resposta);

            foreach (var lista in viewModel.RedesSociais)
            {
                var redeSocial = new RedeSocial(lista.Url, lista.Tipo);
                redeSocial.DefinirUsuario(lista.Usuario);

                avaliacao.RedesSociais.Adicionar(redeSocial);
            }

            if (viewModel.Comprovantes != null)
                foreach (var comprovante in viewModel.Comprovantes)
                {
                    var arquivoComprovante = ArmazenarArquivo(comprovante);
                    avaliacao.Comprovantes.Add(arquivoComprovante);
                }

            _avaliacaoRepository.Adicionar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            return CustomResponse(new
            {
                url_painel_principal = Url.Action("Index", "Dashboard"),
                url_avaliacoes = Url.Action("Index", "Avaliacoes")
            });
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return CustomResponse();

            var avaliacao = _avaliacaoRepository.ObterPorId(id);
            if (avaliacao == null)
            {
                NotificarErro("Editar", "Avaliação não encontrado!");
                return CustomResponse();
            }

            var mentores = _mentorRepository.Listar();
            if (mentores == null)
            {
                NotificarErro("Editar", "Mentores não encontrado!");
                return CustomResponse();
            }

            var viewModel = new AlterarAvaliacaoViewModel()
            {
                Id = avaliacao.Id,
                MentorId = avaliacao.MentorId,
                Texto = avaliacao.Texto,
                Positivo = avaliacao.Positivo,
                Expiracao = avaliacao.DataExpiracao.ToShortDateString(),
                Resposta = avaliacao.TextoResposta,
                ComprovantesCadastrados = ListarComprovantes(avaliacao)
            };

            foreach (var mentor in mentores)
                viewModel.Mentores.Add(new MentorViewModel()
                {
                    Id = mentor.Id,
                    Nome = mentor.Nome
                });

            if (avaliacao.RedesSociais.Lista != null)
                foreach (var redeSocial in avaliacao.RedesSociais.Lista)
                    viewModel.RedesSociais.Add(new RedeSocialMentorViewModel()
                    {
                        Tipo = redeSocial.Tipo,
                        Url = redeSocial.Url,
                        Usuario = redeSocial.Usuario
                    });

            //var viewModel = _mapper.Map<DetalhesAvaliacaoViewModel>(_avaliacaoRepository.ObterPorId(id));
            //if (viewModel == null)
            //{
            //    NotificarErro("Editar", "Avaliação não encontrado!");
            //    return CustomResponse();
            //}

            return PartialView("_Editar", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(AlterarAvaliacaoViewModel viewModel)
        {
            if (viewModel == null || !ModelState.IsValid)
                return BadRequest();

            var avaliacao = _avaliacaoRepository.ObterPorId(viewModel.Id);
            if (avaliacao == null)
            {
                NotificarErro("Erro", "Mentor não encontrado!");
                return CustomResponse();
            }

            if (viewModel.Positivo)
                avaliacao.Positivar();
            else
                avaliacao.Negativar();

            avaliacao.NovoTexto(viewModel.Texto);

            if (!string.IsNullOrEmpty(viewModel.Resposta))
                avaliacao.Responder(viewModel.Resposta);
            else
                avaliacao.RemoverResposta();

            avaliacao.RedesSociais.LimparRedesSociais();
            foreach (var lista in viewModel.RedesSociais)
            {
                var redeSocial = new RedeSocial(lista.Url, lista.Tipo);
                redeSocial.DefinirUsuario(lista.Usuario);

                avaliacao.RedesSociais.Adicionar(redeSocial);
            }

            if (viewModel.Comprovantes != null)
                foreach (var comprovante in viewModel.Comprovantes)
                {
                    var arquivoComprovante = ArmazenarArquivo(comprovante);
                    avaliacao.Comprovantes.Add(arquivoComprovante);
                }

            if (!OperacaoValida())
                return CustomResponse();

            _avaliacaoRepository.Atualizar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            return CustomResponse(new
            {
                url_painel_principal = Url.Action("Index", "Dashboard"),
                url_avaliacoes = Url.Action("Index", "Avaliacoes"),
            });
        }

        [HttpGet]
        public IActionResult QuantidadeAvaliacoes()
        {
            var viewModel = new DetalhesAvaliacaoItemViewModel();
            var avaliacoes = _avaliacaoRepository.Listar();

            viewModel.QuantidadeAvaliacoes = new QtantidadeAvaliacoesViewModel()
            {
                Qtde = avaliacoes.Count(),
                QtdeReferenteMesPassado = 0,
                Texto = ""
            };

            return PartialView("~/Views/Dashboard/_QtdeAvaliacoesCadastradas.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult Detalhes(string id)
        {
            if (string.IsNullOrEmpty(id))
                return CustomResponse();

            var avaliacao = _avaliacaoRepository.ObterPorId(id);
            if (avaliacao == null)
            {
                NotificarErro("Editar", "Avaliação não encontrado!");
                return CustomResponse();
            }


            var comprovantes = ListarComprovantes(avaliacao);

            var viewModel = new DetalhesAvaliacaoViewModel()
            {
                Id = avaliacao.Id,
                AtualizadoAs = avaliacao.AtualizadoAs,
                CriadoAs = avaliacao.CriadoAs,
                DataExpiracao = avaliacao.DataExpiracao,
                Expirado = avaliacao.Expirado,
                DataResposta = avaliacao.DataResposta,
                Respondido = avaliacao.Respondido,
                HorasParaResposta = avaliacao.HorasParaResposta,
                Inativo = avaliacao.Inativo,
                Positivo = avaliacao.Positivo,
                RedesSociais = avaliacao.RedesSociais,
                Texto = avaliacao.Texto,
                Resposta = avaliacao.TextoResposta,
                Mentor = _mentorRepository.ObterPorId(avaliacao.MentorId).Nome,
                Publicado = avaliacao.Publicado,
                Comprovantes = comprovantes
            };

            //var viewModel = _mapper.Map<DetalhesAvaliacaoViewModel>(_avaliacaoRepository.ObterPorId(id));
            //if (viewModel == null)
            //{
            //    NotificarErro("Editar", "Avaliação não encontrado!");
            //    return CustomResponse();
            //}

            return PartialView("_Detalhes", viewModel);
        }

        [HttpGet]
        public IActionResult Inativar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var viewModel = _mapper.Map<IdAvaliacaoViewModel>(_avaliacaoRepository.ObterPorId(id));
            if (viewModel == null)
            {
                NotificarErro("Inativar", "Falha ao inativar avaliação!");
                return CustomResponse();
            }

            if (!OperacaoValida())
                return CustomResponse();

            return PartialView("_Inativar", viewModel);
        }

        [HttpPost, ActionName("Inativar")]
        public IActionResult InativarConfirmado([FromBody] IdAvaliacaoViewModel viewModel)
        {
            var avaliacao = _avaliacaoRepository.ObterPorId(viewModel.Id);

            if (avaliacao == null)
                return NotFound();

            _avaliacaoRepository.Inativar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            if (!OperacaoValida())
                return CustomResponse();

            return CustomResponse(new
            {
                url_avaliacoes = Url.Action("Index", "Avaliacoes"),
            });
        }

        [HttpGet]
        public IActionResult Ativar(IdAvaliacaoViewModel viewModel)
        {
            var avaliacao = _avaliacaoRepository.ObterPorId(viewModel.Id);

            if (avaliacao == null)
                return NotFound();

            _avaliacaoRepository.Ativar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoverPublicacao(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var viewModel = _mapper.Map<IdAvaliacaoViewModel>(_avaliacaoRepository.ObterPorId(id));
            if (viewModel == null)
            {
                NotificarErro("RemoverPublicacao", "Avaliação não encontrada!");
                return CustomResponse();
            }

            if (!OperacaoValida())
                return CustomResponse();

            return PartialView("_RemoverPublicacao", viewModel);
        }

        [HttpPost, ActionName("RemoverPublicacao")]
        public IActionResult RemoverPublicacao([FromBody] IdAvaliacaoViewModel viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            var avaliacao = _avaliacaoRepository.ObterPorId(viewModel.Id);
            if (avaliacao == null)
                return NotFound();

            avaliacao.RemoverPublicacao();

            _avaliacaoRepository.Atualizar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            return CustomResponse(new
            {
                lista_avaliacoes_publicadas = Url.Action("Publicadas", "Avaliacoes"),
            });
        }

        [HttpGet]
        public IActionResult GerarPublicacao(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                NotificarErro("GerarPublicacao", "Avaliação não encontrada!");
                return CustomResponse();
            }

            var avaliacao = _avaliacaoRepository.ObterPorId(id);
            if (avaliacao == null)
            {
                NotificarErro("GerarPublicacao", "Avaliação não encontrada!");
                return CustomResponse();
            }

            var mentor = _mentorRepository.ObterPorId(avaliacao.MentorId);
            if (mentor == null)
            {
                NotificarErro("GerarPublicacao", "Mentor não encontrado!");
                return CustomResponse();
            }

            var redeSocialMentor = mentor.RedesSociais.Lista.FirstOrDefault(x => x.Tipo == ERedeSocialTipo.INSTAGRAM);
            if (redeSocialMentor.Usuario == null)
            {
                NotificarErro("GerarPublicacao", "Mentor não possui nome de usuario na rede social, verifique os dados cadastrados!");
                return CustomResponse();
            }

            if (!OperacaoValida())
                return CustomResponse();

            var viewModel = new PublicacaoViewModels()
            {
                AvaliacaoId = avaliacao.Id,
                MentorId = mentor.Id,
                Publicada = avaliacao.Publicado,
                Mentor = _publicacaoService.Mentor(avaliacao.Positivo, redeSocialMentor.Usuario),
                Avaliacao = _publicacaoService.Avaliacao(avaliacao.Positivo, avaliacao.Texto),
                Resposta = _publicacaoService.RespostaMentor(avaliacao.Positivo, avaliacao.TextoResposta)
            };

            return PartialView("~/Views/Avaliacoes/_Publicacao.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Publicar([FromBody] PublicacaoViewModels viewModel)
        {
            if (viewModel == null)
            {
                NotificarErro("Publicar", "Avaliação não informada!");
                return CustomResponse();
            }

            var avaliacao = _avaliacaoRepository.ObterPorId(viewModel.AvaliacaoId);
            if (avaliacao == null)
            {
                NotificarErro("GerarPublicacao", "Avaliação não encontrada!");
                return CustomResponse();
            }

            if (viewModel.Publicada)
                avaliacao.Publicar();
            else
                avaliacao.RemoverPublicacao();

            _avaliacaoRepository.Atualizar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            return Ok();
        }


        [HttpGet]
        public IActionResult DownloadComprovante(string avaliacaoId, string comprovanteId)
        {
            if (string.IsNullOrEmpty(avaliacaoId) || string.IsNullOrEmpty(comprovanteId))
                return CustomResponse();

            var avaliacao = _avaliacaoRepository.ObterPorId(avaliacaoId);
            if (avaliacao == null)
            {
                NotificarErro("DownloadComprovante", "Avaliação não encontrada!");
                return CustomResponse();
            }

            var arquivo = avaliacao.Comprovantes.FirstOrDefault(x => x.Identificador == comprovanteId);
            if (arquivo == null)
            {
                NotificarErro("DownloadComprovante", "Comprovante não encontrado!");
                return CustomResponse();
            }

            byte[] bytes = _azureStorageService.Download(arquivo.Identificador, EContainerAzureStorage.Comprovantes.ToString());
            if (bytes == null)
            {
                NotificarErro("DownloadComprovante", "Falha ao fazer download do arquivo!");
                return CustomResponse();
            }

            if (!OperacaoValida())
                return CustomResponse();

            return File(bytes, "application/octet-stream", arquivo.Nome);
        }

        [HttpGet]
        public IActionResult RemoverComprovante(string avaliacaoId, string comprovanteId)
        {
            if (string.IsNullOrEmpty(avaliacaoId) || string.IsNullOrEmpty(comprovanteId))
                return CustomResponse();

            var avaliacao = _avaliacaoRepository.ObterPorId(avaliacaoId);
            if (avaliacao == null)
            {
                NotificarErro("DownloadComprovante", "Avaliação não encontrada!");
                return CustomResponse();
            }

            var arquivo = avaliacao.Comprovantes.FirstOrDefault(x => x.Identificador == comprovanteId);
            if (arquivo == null)
            {
                NotificarErro("DownloadComprovante", "Comprovante não encontrado!");
                return CustomResponse();
            }

            _azureStorageService.Remover(arquivo.Identificador, EContainerAzureStorage.Comprovantes.ToString());
            avaliacao.Comprovantes.Remove(arquivo);

            if (!OperacaoValida())
                return CustomResponse();

            _avaliacaoRepository.Atualizar(avaliacao);
            _avaliacaoRepository.SalvarAlteracoes();

            return RedirectToAction("Index", "Dashboard");
        }

        #region Métodos Privados
        private Arquivo ArmazenarArquivo(IFormFile formFile)
        {
            var arquivo = new Arquivo(formFile.FileName, formFile.ContentType);
            _azureStorageService.SalvarArquivo(formFile.GetByteArray(), arquivo.Identificador, EContainerAzureStorage.Comprovantes.ToString()).Wait();
            return arquivo;
        }

        private List<ArquivoViewModel> ListarComprovantes(Avaliacao avaliacao)
        {
            var comprovantes = new List<ArquivoViewModel>();
            if (avaliacao.Comprovantes.Count > 0)
                foreach (var arquivo in avaliacao.Comprovantes)
                {
                    var comprovante = _azureStorageService.ObterUri(arquivo.Identificador, EContainerAzureStorage.Comprovantes.ToString());
                    if (string.IsNullOrEmpty(comprovante))
                        NotificarErro("Comprovante", "Comprovante não encontrado na pasta!");

                    comprovantes.Add(new ArquivoViewModel()
                    {
                        ComprovanteId = arquivo.Identificador,
                        Cadastro = arquivo.Cadastro,
                        AvaliacaoId = avaliacao.Id,
                        ContentType = arquivo.ContentType,
                        Nome = arquivo.Nome,
                        Url = comprovante
                    });
                }

            return comprovantes;
        }
        #endregion
    }
}
