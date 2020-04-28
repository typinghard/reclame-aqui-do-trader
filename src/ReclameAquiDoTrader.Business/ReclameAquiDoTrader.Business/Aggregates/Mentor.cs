using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Aggregates
{
    public class Mentor : Entity, IAggregateRoot
    {
        private Mentor() { }
        public Mentor(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
        public RedeSocialList RedesSociais { get; private set; }
        public List<string> _areasAtuacao { get; private set; }
        public IReadOnlyCollection<string> AreasDeAtuacao { get { return _areasAtuacao; } }
        public EmailList Emails { get; private set; }
        public string Site { get; private set; } //Se quiser V.O
        public TelefoneList Telefones { get; private set; }
    }
}
