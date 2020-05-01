using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Statics;
using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReclameAquiDoTrader.Business.Aggregates
{
    public class Avaliacao : Entity, IAggregateRoot
    {
        private Avaliacao() { }
        public Avaliacao(string mentorId, string texto)
        {
            MentorId = mentorId;
            Texto = texto;
            HorasParaResposta = AvaliacaoStatics.TEMPO_EM_HORAS_PARA_EXPIRACAO;

            Links = new RedeSocialList();
        }

        public string MentorId { get; private set; }
        public string Texto { get; private set; }
        public int HorasParaResposta { get; private set; }
        public bool Positivo { get; private set; }
        public void Positivar()
        {
            Positivo = true;
        }
        public void Negativar()
        {
            Positivo = false;
        }
        public bool Respondido { get; private set; }
        public void MarcarComoRespondido()
        {
            Respondido = true;
        }
        private List<string> _tags { get; set; }
        public IReadOnlyCollection<string> Tags { get { return _tags; } }
        public void AdicionarTag(string tag)
        {
            if (string.IsNullOrEmpty(tag?.Trim()))
                throw new DomainException("Tag não pode ser vazia");

            tag = tag.Trim();

            if (_tags == null)
                _tags = new List<string>();

            if (_tags.Any(t => t.ToLower() == tag.ToLower()))
                return;

            _tags.Add(tag);
        }
        public RedeSocialList Links { get; private set; }
        public bool Expirado { get { return DataExpiracao < DateTime.UtcNow; } }
        public DateTime DataExpiracao { get { return CriadoAs.AddHours(HorasParaResposta); } }
    }
}
