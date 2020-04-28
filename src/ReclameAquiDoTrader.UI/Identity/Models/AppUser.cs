using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Identity.Models
{
    public class AppUser : Raven.Identity.IdentityUser
    {
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";

        /// <summary>
        /// A user's full name.
        /// </summary>
        public string FullName { get; set; }
    }
}
