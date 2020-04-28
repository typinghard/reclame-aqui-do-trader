using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Identity.Models
{
    public class RavenDBIdentityUser
    {
        public RavenDBIdentityUser(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public string Id { get; internal set; }
        public string UserName { get; internal set; }
        public string NormalizedUserName { get; internal set; }
    }
}
