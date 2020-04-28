using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class TelefoneList
    {
        public List<Telefone> _telefones { get; set; }
        public IReadOnlyCollection<Telefone> Telefones { get { return _telefones; } }
    }
    public class Telefone
    {
        public string Numero { get; }
        public string DDD { get { return Numero?.Substring(0, 2); } }

        public Telefone(string numero)
        {
            //if (!numero.Contains(REGEX)) throw new Exception("Telefone é inválido");

            Numero = numero;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Telefone;

            return other != null ? Equals(other) : Equals(obj as string);
        }
        public bool Equals(Telefone other) => other != null && Numero == other.Numero;
        public bool Equals(string other) => Numero == other;
        public static bool operator ==(Telefone a, Telefone b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;

            return a.Numero == b.Numero;
        }
        public static bool operator !=(Telefone a, Telefone b) => !(a == b);
        public override int GetHashCode() => Numero.GetHashCode();
        public override string ToString() => Numero;
    }
}
