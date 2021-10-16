using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels
{
    public class AlterarMentorViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Site { get; set; }
        public List<string> AreasDeAtuacao { get; set; }

        public List<TelefoneViewModel> Telefones { get; set; }
        public List<EmailViewModel> Emails { get; set; }
        public List<RedeSocialViewModel> RedesSociais { get; set; }
    }
}
