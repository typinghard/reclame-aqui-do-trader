using ReclameAquiDoTrader.Business.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class EmailList 
    {
        private List<Email> _emails { get; set; }
        public IReadOnlyCollection<Email> Emails { get { return _emails; } }
        public void Adicionar(Email email)
        {
            if (_emails == null)
                _emails = new List<Email>();

            if (_emails.Any(e => e == email))
                return;
            _emails.Add(email);
        }
    }
    public class Email : ValueObject<Email>
    {
        public string Endereco { get; }

        public Email(string value)
        {
            if (!value.Contains("@")) throw new Exception("Email é inválido");

            Endereco = value;
        }
        public override string ToString() => Endereco;
    }
}
