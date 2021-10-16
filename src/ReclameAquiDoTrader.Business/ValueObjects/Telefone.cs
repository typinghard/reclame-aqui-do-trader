using Newtonsoft.Json;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class TelefoneList
    {
        private List<Telefone> _telefones { get; set; }
        [JsonIgnore]
        public IReadOnlyCollection<Telefone> Telefones { get { return _telefones; } }
        public void Adicionar(Telefone telefone)
        {
            if (_telefones == null)
                _telefones = new List<Telefone>();

            if (_telefones.Any(t => t == telefone))
                return;

            _telefones.Add(telefone);
        }
        public void LimparTelefones()
        {
            _telefones?.Clear();
        }
    }
    public class Telefone : ValueObject<Telefone>
    {
        public string Numero { get; }
        public string DDD { get { return Numero?.Substring(0, 2); } }

        public Telefone(string numero)
        {
            //if (!numero.Contains(REGEX)) throw new Exception("Telefone é inválido");

            Numero = numero
                        .Replace(" ", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("+", "")
                        .Replace("-", "");
        }

        public override string ToString() => Numero;
    }
}
