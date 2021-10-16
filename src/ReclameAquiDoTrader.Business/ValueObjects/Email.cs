using Newtonsoft.Json;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class EmailList
    {

        public List<Email> _emails { get; set; }
        [JsonIgnore]
        public List<Email> Emails { get { return _emails; } }
        public void Adicionar(Email email)
        {
            if (_emails == null)
                _emails = new List<Email>();

            if (_emails.Any(e => e == email))
                return;

            _emails.Add(email);
        }
        public void LimparEmails()
        {
            _emails?.Clear();
        }
    }
    public class Email : ValueObject<Email>
    {
        private Email() { }

        public string Endereco { get; private set; }

        public Email(string value)
        {
            if (!ValidarEmail(value)) throw new DomainException("Email é inválido");

            Endereco = value;
        }
        public override string ToString() => Endereco;

        private bool ValidarEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
