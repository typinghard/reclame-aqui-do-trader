using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.UI.ViewModels.PesquisaViewModels
{
    public class AssinarNewsletterViewModel
    {
        [Required(ErrorMessage = "Por favor, informe o whatsapp")]
        public string Whatsapp { get; set; }
    }
}
