using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ReclameAquiDoTrader.UI.ViewModels.AcessoViewModel
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Campo {0} inválido!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string Token { get; set; }

        public bool TokenValido { get; set; }
    }
}
