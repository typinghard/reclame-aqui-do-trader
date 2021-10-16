using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Enums;

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
