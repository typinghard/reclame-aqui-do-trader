using ReclameAquiDoTrader.Business.Core.DomainObjects;

namespace ReclameAquiDoTrader.Business.Aggregates
{
    public class Assinante : Entity, IAggregateRoot
    {
        public Assinante(string whatsapp)
        {
            Whatsapp = whatsapp;
        }
        public string Whatsapp { get; set; }
    }
}
