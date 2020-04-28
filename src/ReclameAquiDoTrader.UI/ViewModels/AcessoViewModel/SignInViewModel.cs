using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
