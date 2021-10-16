using ReclameAquiDoTrader.Business.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels
{
    public class CriarMentorViewModel
    {
        [Required]
        public string Nome { get; set; }
        public string Site { get; set; }
        public List<string> AreasDeAtuacao { get; set; }

        public List<TelefoneViewModel> Telefones { get; set; }
        public List<EmailViewModel> Emails { get; set; }
        [Required]
        public List<RedeSocialViewModel> RedesSociais { get; set; }
    }

    public class RedeSocialViewModel
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public ERedeSocialTipo Tipo { get; set; }
    }


    public class TelefoneViewModel
    {
        public string Numero { get; set; }
    }

    public class EmailViewModel
    {
        public string Endereco { get; set; }
    }

}

