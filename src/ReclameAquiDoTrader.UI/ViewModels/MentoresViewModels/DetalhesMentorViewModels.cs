using System;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels
{
    public class DetalhesMentorViewModels
    {
        public DetalhesMentorViewModels()
        {
            AreasAtuacaoExistentes = new List<string>();
        }
        public string Id { get; set; }
        public DateTime CriadoAs { get; set; }
        public DateTime AtualizadoAs { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public bool Inativo { get; set; }
        public List<string> AreasDeAtuacao { get; set; }

        public List<TelefoneViewModel> Telefones { get; set; }
        public List<EmailViewModel> Emails { get; set; }
        public List<RedeSocialViewModel> RedesSociais { get; set; }

        public List<string> AreasAtuacaoExistentes { get; set; }
    }
}