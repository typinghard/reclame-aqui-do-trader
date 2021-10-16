using Bogus;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Enums;
using ReclameAquiDoTrader.Business.Interfaces.Data;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.UI.Extensions;
using ReclameAquiDoTrader.UI.ViewModels.AssinantesViewModels;
using ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels;
using ReclameAquiDoTrader.UI.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class DashboardController : MainController
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IMentorRepository _mentorRepository;
        private readonly IAssinanteRepository _assinanteRepository;

        private readonly IUnitOfWork _unitOfWork;
        public DashboardController(
            IUnitOfWork unitOfWork,
            IAvaliacaoRepository avaliacaoRepository,
            IMentorRepository mentorRepository,
            IAssinanteRepository assinanteRepository,
            INotificador notificador) : base(notificador)
        {
            _assinanteRepository = assinanteRepository;
            _unitOfWork = unitOfWork;
            _avaliacaoRepository = avaliacaoRepository;
            _mentorRepository = mentorRepository;
        }

        private void AlimentarBase()
        {
            var AreasAtuacao = new List<string>()
            {
                "Tesouro Direto",
                "Tesouro Direto",
                "Tesouro Direto",
                "Tesouro Direto",
                "Tesouro Direto",
                "Gráficos",
                "Gráficos",
                "Gráficos",
                "Gráficos",
                "Ações",
                "Ações",
                "Daytrade",
                "Poupança",
                "Fundos de investimento",
                "CDB",
                "Forex"
            };

            var Tags = new List<string>()
            {
                "sucesso", "dinheiro", "investimento", "topzera", "ta_bom", "muito_bom"
            };

            var idsMentor = new List<string>();

            AdicionarAssinantes();

            for (int i = 0; i < 30; i++)
            {
                var faker = new Faker("pt_BR");
                var mentor = new Mentor(faker.Person.FullName);
                mentor.AtualizadoAs = mentor.CriadoAs = faker.Date.Recent(60);

                for (int j = 0; j < faker.Random.Int(1, 3); j++)
                {
                    mentor.AdicionarAreaDeAtuacao(faker.PickRandom(AreasAtuacao));
                }
                mentor.AtribuirSite(faker.Internet.Url());
                for (int j = 0; j < faker.Random.Int(1, 3); j++)
                {
                    mentor.Emails.Adicionar(new Business.ValueObjects.Email(faker.Internet.Email()));
                }
                for (int j = 0; j < faker.Random.Int(1, 2); j++)
                {
                    //faker.Random.Enum<ERedeSocialTipo>()
                    var redeSocial = new Business.ValueObjects.RedeSocial(faker.Internet.Url(), ERedeSocialTipo.INSTAGRAM);
                    redeSocial.DefinirUsuario(faker.Random.String(1, 30, 'a', 'z').Trim().ToLower().Replace(" ", "."));

                    mentor.RedesSociais.Adicionar(redeSocial);
                }
                for (int j = 0; j < faker.Random.Int(1, 2); j++)
                {
                    mentor.Telefones.Adicionar(new Business.ValueObjects.Telefone(faker.Phone.PhoneNumber("###########")));
                }
                idsMentor.Add(mentor.Id);
                _mentorRepository.Adicionar(mentor);
            }

            for (int i = 0; i < 200; i++)
            {
                var faker = new Faker("pt_BR");
                var avaliacao = new Avaliacao(faker.PickRandom(idsMentor), faker.Lorem.Paragraph(2));
                avaliacao.AtualizadoAs = avaliacao.CriadoAs = faker.Date.Recent(60);

                if (faker.Random.Int(0, 1) == 1)
                {
                    avaliacao.Positivar();
                }
                if (faker.Random.Int(0, 1) == 1)
                {
                    avaliacao.Responder(faker.Lorem.Paragraph(2));
                    avaliacao.Publicar();
                }

                if (faker.Random.Int(0, 1) == 1 && !avaliacao.Publicado)
                    avaliacao.Publicar();

                //faker.Random.Enum<ERedeSocialTipo>()
                avaliacao.RedesSociais.Adicionar(new Business.ValueObjects.RedeSocial(faker.Internet.Url(), ERedeSocialTipo.INSTAGRAM));
                _avaliacaoRepository.Adicionar(avaliacao);
            }

            _unitOfWork.Commit();
        }

        private void AdicionarAssinantes()
        {
            for (int i = 0; i < 11; i++)
            {
                var faker = new Faker("pt_BR");
                var assinante = new Assinante(faker.Phone.PhoneNumber("###########"));
                assinante.AtualizadoAs = assinante.CriadoAs = faker.Date.Recent(60);

                _assinanteRepository.Adicionar(assinante);
            }
        }


        public IActionResult Index()
        {
            //AlimentarBase();
            var viewModel = new DashboardItemViewModels();

            var avaliacoes = _avaliacaoRepository.Listar().ToList();
            var mentores = _mentorRepository.Listar().ToList();
            var assinantes = _assinanteRepository.Listar().ToList();

            viewModel.QtdeTotalAvaliacoes = avaliacoes.Count();
            viewModel.QtdeTotalMentores = mentores.Count();
            viewModel.QtdeTotalAssinantes = assinantes.Count();

            viewModel.DadosGrafico = CarregaDadosGrafico(avaliacoes, mentores, assinantes);
            viewModel.AvaliacoesPendentesPublicacao = AvaliacoesPendentesPublicacao(avaliacoes, mentores);
            viewModel.QtdeAvaliacoesPendentesPublicacao = avaliacoes.AvaliacoesPendentesDePublicacao().Count();
            viewModel.AvaliacoesEmAndamento = AvaliacoesEmAndamento(avaliacoes, mentores);
            viewModel.QtdeAvaliacoesEmAndamento = avaliacoes.AvaliacoesEmAndamento().Count();
            viewModel.UltimasAvaliacoesCadastradas = Top5AvaliacoesCadastradas(avaliacoes, mentores);
            viewModel.UltimosMentoresCadastrados = Top5MentoresCadastrados(mentores);
            viewModel.UltimosAssinantesCadastrados = Top5AssinantesCadastrados(assinantes);
            viewModel.Avaliacoes = DetalhesAvaliacoes(avaliacoes, mentores);

            return View(viewModel);
        }

        private long GetJavascriptTimestamp(System.DateTime input)
        {
            System.TimeSpan span = new System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks);
            System.DateTime time = input.Subtract(span);
            return (long)(time.Ticks / 10000);
        }

        private List<DadosGrafico> CarregaDadosGrafico(List<Avaliacao> avaliacoes, List<Mentor> mentores, List<Assinante> assinantes)
        {
            var dias = -30;

            #region Dados  Agrupados do Grafico do Dashboard
            var dadosGraficoAvaliacoesCadastradas = avaliacoes.Where(x => x.CriadoAs >= DateTime.UtcNow.AddDays(dias)
                                                                       && x.CriadoAs <= DateTime.UtcNow)
                                                              .GroupBy(x => x.CriadoAs.ToShortDateString())
                                                              .Select(x => new
                                                              {
                                                                  CriadoAs = x.Key,
                                                                  Qtde = x.Count()
                                                              })
                                                              .OrderByDescending(x => x.CriadoAs);

            var dadosGraficoMentoresCadastradas = mentores.Where(x => x.CriadoAs >= DateTime.UtcNow.AddDays(dias)
                                                                   && x.CriadoAs <= DateTime.UtcNow)
                                                          .GroupBy(x => x.CriadoAs.ToShortDateString())
                                                          .Select(x => new
                                                          {
                                                              CriadoAs = x.Key,
                                                              Qtde = x.Count()
                                                          });

            var dadosGraficoAssinantesCadastradas = assinantes.Where(x => x.CriadoAs >= DateTime.UtcNow.AddDays(dias)
                                                                       && x.CriadoAs <= DateTime.UtcNow)
                                                               .GroupBy(x => x.CriadoAs.ToShortDateString())
                                                               .Select(x => new
                                                               {
                                                                   CriadoAs = x.Key,
                                                                   Qtde = x.Count()
                                                               })
                                                               .OrderByDescending(x => x.CriadoAs);
            #endregion

            List<DadosGrafico> dadosGrafico = new List<DadosGrafico>();

            var pos = 0;
            foreach (var item in dadosGraficoAvaliacoesCadastradas)
            {
                var dataConvertida = Convert.ToDateTime(item.CriadoAs);
                dadosGrafico.Add(new DadosGrafico()
                {
                    Indice = pos,
                    CriadoAs = dataConvertida,
                    DataTicks = GetJavascriptTimestamp(dataConvertida),
                    Data = dataConvertida.ToString("dd/MM"),
                    QtdeAvaliacao = item.Qtde,
                    QtdeMentor = 0,
                    QtdeAssinante = 0,
                });

                pos++;
            }

            pos = 0;
            foreach (var item in dadosGraficoMentoresCadastradas)
            {
                var dataConvertida = Convert.ToDateTime(item.CriadoAs);
                if (dadosGrafico.Count > 0 && dadosGrafico.Any(x => x.CriadoAs == dataConvertida))
                {
                    var dadoGrafico = dadosGrafico.FirstOrDefault(x => x.CriadoAs == dataConvertida);
                    if (dadoGrafico != null)
                        dadoGrafico.QtdeMentor = item.Qtde;
                }
                else
                    dadosGrafico.Add(new DadosGrafico()
                    {
                        Indice = pos,
                        CriadoAs = dataConvertida,
                        DataTicks = GetJavascriptTimestamp(dataConvertida),
                        Data = dataConvertida.ToString("dd/MM"),
                        QtdeAvaliacao = 0,
                        QtdeMentor = item.Qtde,
                        QtdeAssinante = 0,
                    });


                pos++;
            }

            pos = 0;
            foreach (var item in dadosGraficoAssinantesCadastradas)
            {
                var dataConvertida = Convert.ToDateTime(item.CriadoAs);
                if (dadosGrafico.Count > 0 && dadosGrafico.Any(x => x.CriadoAs == dataConvertida))
                {
                    var dadoGrafico = dadosGrafico.FirstOrDefault(x => x.CriadoAs == dataConvertida);
                    if (dadoGrafico != null)
                        dadoGrafico.QtdeAssinante = item.Qtde;
                }
                else
                    dadosGrafico.Add(new DadosGrafico()
                    {
                        Indice = pos,
                        CriadoAs = dataConvertida,
                        DataTicks = GetJavascriptTimestamp(dataConvertida),
                        Data = dataConvertida.ToString("dd/MM"),
                        QtdeAvaliacao = 0,
                        QtdeMentor = 0,
                        QtdeAssinante = item.Qtde,
                    });

                pos++;
            }

            return dadosGrafico.OrderBy(x => x.CriadoAs).ToList();

        }


        private List<AvaliacoesEmAndamentoViewModel> AvaliacoesEmAndamento(List<Avaliacao> avaliacoes, List<Mentor> mentores)
        {
            List<AvaliacoesEmAndamentoViewModel> retorno = new List<AvaliacoesEmAndamentoViewModel>();

            var avaliacoesEmAndamento = avaliacoes.AvaliacoesEmAndamento().Take(5);

            foreach (var avaliacao in avaliacoesEmAndamento)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                retorno.Add(new AvaliacoesEmAndamentoViewModel()
                {
                    AvaliacaoId = avaliacao.Id,
                    CriadoAs = avaliacao.CriadoAs,
                    DataExpiracao = avaliacao.DataExpiracao,
                    Mentor = mentor.Nome,
                    RedesSociais = avaliacao.RedesSociais
                });
            }

            return retorno;
        }

        private List<AvaliacoesPendentesPublicacaoViewModel> AvaliacoesPendentesPublicacao(List<Avaliacao> avaliacoes, List<Mentor> mentores)
        {
            List<AvaliacoesPendentesPublicacaoViewModel> retorno = new List<AvaliacoesPendentesPublicacaoViewModel>();
            var avaliacoesPendentesPublicacao = avaliacoes.AvaliacoesPendentesDePublicacao().Take(5);

            foreach (var avaliacao in avaliacoesPendentesPublicacao)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                retorno.Add(new AvaliacoesPendentesPublicacaoViewModel()
                {
                    AvaliacaoId = avaliacao.Id,
                    CriadoAs = avaliacao.CriadoAs,
                    DataExpiracao = avaliacao.DataExpiracao,
                    Mentor = mentor.Nome,
                    RedesSociais = avaliacao.RedesSociais,
                    Texto = string.IsNullOrEmpty(avaliacao.Texto) ? false : true,
                    Respondido = string.IsNullOrEmpty(avaliacao.TextoResposta) ? false : true,
                });
            }

            return retorno;
        }

        private List<UltimasAvaliacoesCadastradasItemViewModel> Top5AvaliacoesCadastradas(List<Avaliacao> avaliacoes, List<Mentor> mentores)
        {
            List<UltimasAvaliacoesCadastradasItemViewModel> retorno = new List<UltimasAvaliacoesCadastradasItemViewModel>();

            var avaliacoesCadastradas = avaliacoes.ToList()
                                          .OrderByDescending(o => o.CriadoAs)
                                          .Take(5);

            foreach (var avaliacao in avaliacoesCadastradas)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                retorno.Add(new UltimasAvaliacoesCadastradasItemViewModel()
                {
                    AvaliacaoId = avaliacao.Id,
                    CriadoAs = avaliacao.CriadoAs,
                    Mentor = mentor.Nome,
                    Respondido = avaliacao.Respondido,
                    Positivo = avaliacao.Positivo,
                    RedesSociais = avaliacao.RedesSociais
                });
            }

            return retorno;
        }

        private List<UltimosMentoresCadastradosItemViewModel> Top5MentoresCadastrados(List<Mentor> mentores)
        {
            List<UltimosMentoresCadastradosItemViewModel> retorno = new List<UltimosMentoresCadastradosItemViewModel>();

            var ultimosMentoresCadastrados = mentores.ToList()
                                             .OrderByDescending(o => o.CriadoAs)
                                             .Take(5);

            foreach (var mentor in ultimosMentoresCadastrados)
            {
                retorno.Add(new UltimosMentoresCadastradosItemViewModel()
                {
                    CriadoAs = mentor.CriadoAs,
                    Nome = mentor.Nome,
                    Id = mentor.Id
                });
            }

            return retorno;
        }

        private List<UltimosAssinantesCadastradosViewModel> Top5AssinantesCadastrados(List<Assinante> assinantes)
        {
            List<UltimosAssinantesCadastradosViewModel> retorno = new List<UltimosAssinantesCadastradosViewModel>();

            var ultimosAssiantesCadastrados = assinantes.ToList()
                                             .OrderByDescending(o => o.CriadoAs)
                                             .Take(5);

            foreach (var assinante in ultimosAssiantesCadastrados)
            {
                retorno.Add(new UltimosAssinantesCadastradosViewModel()
                {
                    CriadoAs = assinante.CriadoAs,
                    Telefone = assinante.Whatsapp,
                    Id = assinante.Id
                });
            }

            return retorno;
        }
        private List<DetalhesAvaliacaoViewModel> DetalhesAvaliacoes(List<Avaliacao> avaliacoes, List<Mentor> mentores)
        {
            List<DetalhesAvaliacaoViewModel> retorno = new List<DetalhesAvaliacaoViewModel>();

            var avaliacoesDetalhes = avaliacoes.OrderByDescending(o => o.CriadoAs);

            foreach (var avaliacao in avaliacoesDetalhes)
            {
                var mentor = mentores.First(x => x.Id == avaliacao.MentorId);

                retorno.Add(new DetalhesAvaliacaoViewModel()
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
                    Publicado = avaliacao.Publicado,
                    DataPublicacao = avaliacao.DataPublicacao,
                    RedesSociais = avaliacao.RedesSociais,
                    Texto = avaliacao.Texto,
                    Mentor = mentor.Id
                });
            }

            return retorno;
        }
    }
}
