using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class RedeSocial : ValueObject<RedeSocial>
    {
        private RedeSocial() { }

        public RedeSocial(LinkRedeSocial link)
        {
            Link = link;
        }

        public LinkRedeSocial Link { get; private set; }
        public string Usuario { get; private set; }
        public void DefinirUsuario(string usuario)
        {
            Usuario = usuario;
        }
    }
    public class RedeSocialList
    {
        public void Adicionar(RedeSocial redeSocial)
        {
            if (_redeSocial == null)
                _redeSocial = new List<RedeSocial>();

            if (_redeSocial.Any(r => r == redeSocial))
                return;

            _redeSocial.Add(redeSocial);
        }
        private List<RedeSocial> _redeSocial { get; set; }
        public IReadOnlyCollection<RedeSocial> Lista { get { return _redeSocial; } }
    }
}
