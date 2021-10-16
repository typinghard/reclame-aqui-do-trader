using Microsoft.AspNetCore.Http;
using ReclameAquiDoTrader.Business.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class CriarAvaliacaoViewModel
    {
        [Required]
        public string MentorId { get; set; }
        public string Texto { get; set; }
        public bool Positivo { get; set; }
        public string Expiracao { get; set; }
        public string Resposta { get; set; }
        public List<RedeSocialMentorViewModel> RedesSociais { get; set; } = new List<RedeSocialMentorViewModel>();

        public List<MentorViewModel> Mentores { get; set; } = new List<MentorViewModel>();
        public IList<IFormFile> Comprovantes { get; set; }
    }

    public class RedeSocialMentorViewModel
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public ERedeSocialTipo Tipo { get; set; }
    }

    public class MentorViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
    }
}
