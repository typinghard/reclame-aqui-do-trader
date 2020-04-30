using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class LinkRedeSocial : ValueObject<LinkRedeSocial>
    {
        private LinkRedeSocial() { }

        public LinkRedeSocial(string link, ERedeSocial tipo)
        {
            Link = link;
            Tipo = tipo;
        }

        public string Link { get; set; }
        public ERedeSocial Tipo { get; set; }
    }
}
