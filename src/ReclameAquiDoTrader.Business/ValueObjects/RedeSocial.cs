using Newtonsoft.Json;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class RedeSocial : ValueObject<RedeSocial>
    {
        private RedeSocial() { }

        public RedeSocial(string url, ERedeSocialTipo tipo)
        {
            Tipo = tipo;
            Url = url;
        }

        public string Url { get; private set; }
        public string Usuario { get; private set; }
        public ERedeSocialTipo Tipo { get; private set; }
        public void DefinirUsuario(string usuario)
        {
            Usuario = usuario;
        }
    }
    public class RedeSocialList
    {
        public void Adicionar(RedeSocial redeSocial)
        {
            if (_redeSocial == null)
                _redeSocial = new List<RedeSocial>();

            if (_redeSocial.Any(r => r == redeSocial))
                return;

            _redeSocial.Add(redeSocial);
        }
        public List<RedeSocial> _redeSocial { get; set; }
        [JsonIgnore]
        public IReadOnlyCollection<RedeSocial> Lista { get { return _redeSocial; } }
        public void LimparRedesSociais()
        {
            _redeSocial?.Clear();
        }
    }
}
