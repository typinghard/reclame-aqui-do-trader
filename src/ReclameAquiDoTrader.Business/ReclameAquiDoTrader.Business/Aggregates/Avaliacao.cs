using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Aggregates
{
    public class Avaliacao : Entity, IAggregateRoot
    {
        private Avaliacao() { }
        public Avaliacao(string mentorId, string texto, int horasParaResposta)
        {
            MentorId = mentorId;
            Texto = texto;
            HorasParaResposta = horasParaResposta;
        }

        public string MentorId { get; private set; }
        public string Texto { get; private set; }
        public int HorasParaResposta { get; private set; }
        public bool Positivo { get; private set; }
        public bool Respondido { get; private set; }
        public List<string> _tags { get; private set; }
        public IReadOnlyCollection<string> Tags { get { return _tags; } }
        public RedeSocialList Links { get; private set; }
        public bool Expirado { get { return CriadoAs.AddHours(HorasParaResposta) > DateTime.UtcNow; } }
    }
}
