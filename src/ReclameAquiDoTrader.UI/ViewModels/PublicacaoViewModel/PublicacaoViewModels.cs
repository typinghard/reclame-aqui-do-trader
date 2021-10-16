using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.PublicacaoViewModel
{
    public class PublicacaoViewModels
    {
        public PublicacaoViewModels()
        {
            Avaliacao = new List<TextoAvaliacaoViewModel>();
            Resposta = new List<RespostaMentorViewModel>();
        }
        public string MentorId { get; set; }
        public string AvaliacaoId { get; set; }
        public bool Publicada { get; set; }
        public PublicacaoMentorViewModel Mentor { get; set; }
        public List<TextoAvaliacaoViewModel> Avaliacao { get; set; }
        public List<RespostaMentorViewModel> Resposta { get; set; }
    }

    public class PublicacaoMentorViewModel
    {
        public string ImagemFundo { get; set; }
        public string StyleDinamicoUsuario { get; set; }
        public string Usuario { get; set; }
    }

    public class TextoAvaliacaoViewModel
    {
        public string ImagemFundo { get; set; }
        public string ImgLogoTransparente { get; set; }
        public string ImgPolegar { get; set; }
        public string Texto { get; set; }
        public int PaginaAtual { get; set; }
        public int PaginaFinal { get; set; }
    }

    public class RespostaMentorViewModel
    {
        public string ImagemFundo { get; set; }
        public string ImgLogoTransparente { get; set; }
        public string Resposta { get; set; }
        public int PaginaAtual { get; set; }
        public int PaginaFinal { get; set; }
    }
}
