using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReclameAquiDoTrader.Business.Aggregates
{
    public class Mentor : Entity, IAggregateRoot
    {
        private Mentor() { }
        public Mentor(string nome)
        {
            Nome = nome;
            RedesSociais = new RedeSocialList();
            Emails = new EmailList();
            Telefones = new TelefoneList(); 
        }

        public string Nome { get; private set; }
        public RedeSocialList RedesSociais { get; private set; }
        private List<string> _areasAtuacao { get; set; }
        public IReadOnlyCollection<string> AreasDeAtuacao { get { return _areasAtuacao; } }
        public void AdicionarAreaDeAtuacao(string areaAtuacao)
        {
            if (string.IsNullOrEmpty(areaAtuacao?.Trim()))
                throw new DomainException("Área de atuação não pode ser vazia");

            areaAtuacao = areaAtuacao.Trim();

            if (_areasAtuacao == null)
                _areasAtuacao = new List<string>();

            if (_areasAtuacao.Any(a => a.ToLower() == areaAtuacao.ToLower()))
                return;

            _areasAtuacao.Add(areaAtuacao);
        }
        public EmailList Emails { get; private set; }
        public string Site { get; private set; } 
        public void AtribuirSite(string site)
        {
            Site = site;
        }
        public TelefoneList Telefones { get; private set; }
    }
}
