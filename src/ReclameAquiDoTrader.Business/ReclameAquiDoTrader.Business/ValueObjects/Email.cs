using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class EmailList
    {
        public List<Email> _emails { get; set; }
        public IReadOnlyCollection<Email> Emails { get { return _emails; } }
    }
    public class Email
    {
        public string Endereco { get; }

        public Email(string value)
        {
            if (!value.Contains("@")) throw new Exception("Email é inválido");

            Endereco = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Email;

            return other != null ? Equals(other) : Equals(obj as string);
        }
        public bool Equals(Email other) => other != null && Endereco == other.Endereco;
        public bool Equals(string other) => Endereco == other;
        public static bool operator ==(Email a, Email b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;

            return a.Endereco == b.Endereco;
        }
        public static bool operator !=(Email a, Email b) => !(a == b);
        public override int GetHashCode() => Endereco.GetHashCode();
        public override string ToString() => Endereco;
    }
}
