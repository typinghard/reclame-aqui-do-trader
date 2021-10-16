using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class AlterarAvaliacaoViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string MentorId { get; set; }

        public string Texto { get; set; }
        public bool Positivo { get; set; }
        public string Expiracao { get; set; }
        public string Resposta { get; set; }

        public List<RedeSocialMentorViewModel> RedesSociais { get; set; } = new List<RedeSocialMentorViewModel>();

        public List<MentorViewModel> Mentores { get; set; } = new List<MentorViewModel>();
        public IList<IFormFile> Comprovantes { get; set; }
        public List<ArquivoViewModel> ComprovantesCadastrados { get; set; } = new List<ArquivoViewModel>();
    }
}
