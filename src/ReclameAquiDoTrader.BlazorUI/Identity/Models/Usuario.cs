using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.BlazorUI.Identity.Models
{
    public class Usuario : Raven.Identity.IdentityUser
    {
        public string Nome { get; internal set; }
    }
}
