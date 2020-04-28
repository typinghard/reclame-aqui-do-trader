using ReclameAquiDoTrader.Business.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class RedeSocial
    {
        public LinkRedeSocial Link { get; private set; }
        public string Usuario { get; private set; }
    }
    public class RedeSocialList
    {
        public List<RedeSocial> _redeSocial { get; private set; }
        public IReadOnlyCollection<RedeSocial> Lista { get { return _redeSocial; } }
    }
}
