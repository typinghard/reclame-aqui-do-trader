using Microsoft.AspNetCore.Hosting;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.UI.Extensions;
using ReclameAquiDoTrader.UI.Interfaces;
using ReclameAquiDoTrader.UI.ViewModels.PublicacaoViewModel;
using System;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.Services
{
    public class ArquivosPublicacaoService : IPublicacaoService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotificador _notificador;

        public ArquivosPublicacaoService(INotificador notificador,
                                         IWebHostEnvironment webHostEnvironment)
        {
            _notificador = notificador;
            _webHostEnvironment = webHostEnvironment;
        }

        public PublicacaoMentorViewModel Mentor(bool positivo, string nomeUsuarioRedeSocial)
        {
            var viewModel = new PublicacaoMentorViewModel();

            if (string.IsNullOrEmpty(nomeUsuarioRedeSocial))
            {
                _notificador.Handle(new Notificacao("Mentor", "Nome de usuario não informado."));
                return null;
            }

            viewModel.StyleDinamicoUsuario = nomeUsuarioRedeSocial.StyleDinamicoNomeMentorPublicacao(positivo);
            viewModel.ImagemFundo = string.Concat(@"\img\", "pag_1_vazia.jpg");
            viewModel.Usuario = $"@{nomeUsuarioRedeSocial}";

            return viewModel;
        }

        public List<TextoAvaliacaoViewModel> Avaliacao(bool positivo, string texto)
        {
            List<TextoAvaliacaoViewModel> retorno = new List<TextoAvaliacaoViewModel>();

            if (string.IsNullOrEmpty(texto))
                return retorno;

            var pastaImg = @"\img\";
            var caracterMaximoPagina = 1342;
            double pagMax = CalcularQtdePagina(texto, caracterMaximoPagina);
            var caracterMinimo = 0;
            var caracterMaximo = CalcularCaractereMaximo(texto, caracterMinimo, (int)pagMax, caracterMaximoPagina);

            for (int i = 0; i < pagMax; i++)
            {
                var pagina = texto.Substring(caracterMinimo, caracterMaximo);
                int aux = i + 1;

                var viewModel = new TextoAvaliacaoViewModel()
                {
                    ImagemFundo = string.Concat(pastaImg, "pag_texto_vazia.jpg"),
                    ImgLogoTransparente = string.Concat(pastaImg, "logo-transparente.png"),
                    ImgPolegar = positivo ? string.Concat(pastaImg, "Positivo.png") : string.Concat(pastaImg, "Negativo.png"),
                    PaginaAtual = aux,
                    PaginaFinal = (int)pagMax,
                    Texto = pagina,
                };

                retorno.Add(viewModel);

                if (i != (pagMax - 1))
                {
                    caracterMinimo = CalcularCaractereMinimo(caracterMinimo, caracterMaximoPagina);
                    caracterMaximo = CalcularCaractereMaximo(texto, caracterMinimo, (int)pagMax, caracterMaximoPagina);
                }
            }

            return retorno;
        }

        public List<RespostaMentorViewModel> RespostaMentor(bool positivo, string respostaMentor)
        {
            List<RespostaMentorViewModel> retorno = new List<RespostaMentorViewModel>();

            if (string.IsNullOrEmpty(respostaMentor))
                return retorno;

            var caracterMaximoPagina = 1342;
            double pagMax = CalcularQtdePagina(respostaMentor, caracterMaximoPagina);
            var caracterMinimo = 0;
            var caracterMaximo = CalcularCaractereMaximo(respostaMentor, caracterMinimo, (int)pagMax, caracterMaximoPagina);

            for (int i = 0; i < pagMax; i++)
            {
                var pagina = respostaMentor.Substring(caracterMinimo, caracterMaximo);
                int aux = i + 1;

                var viewModel = new RespostaMentorViewModel()
                {
                    ImagemFundo = string.Concat(@"\img\", "resposta_mentor_vazia.jpg"),
                    Resposta = pagina,
                    PaginaAtual = aux,
                    PaginaFinal = (int)pagMax
                };

                retorno.Add(viewModel);

                if (i != (pagMax - 1))
                {
                    caracterMinimo = CalcularCaractereMinimo(caracterMinimo, caracterMaximoPagina);
                    caracterMaximo = CalcularCaractereMaximo(respostaMentor, caracterMinimo, (int)pagMax, caracterMaximoPagina);
                }
            }

            return retorno;
        }

        #region Métodos de Apoio

        public int CalcularQtdePagina(string texto, int caracterMaximoPagina)
        {
            double pagMax = Math.Round((double)texto.Length / (double)caracterMaximoPagina, 2);
            pagMax = (pagMax % 1) == 0 ? pagMax : pagMax + 1;
            pagMax = pagMax == 0 ? 1 : pagMax;

            return (int)pagMax;
        }

        public int CalcularCaractereMinimo(int caracterMinimoAtual, int caracterMaximoAtual)
        {
            return (caracterMinimoAtual + 1) + caracterMaximoAtual;
        }

        public int CalcularCaractereMaximo(string textoCompleto, int caractereMinimoAtual, int pagMax, int caracteresMaximoPagina)
        {
            var soma = textoCompleto.Length - (caractereMinimoAtual + caracteresMaximoPagina);

            if (soma < 0 || pagMax == 1)
                caracteresMaximoPagina = textoCompleto.Length - caractereMinimoAtual;

            return caracteresMaximoPagina;
        }
        #endregion
    }
}