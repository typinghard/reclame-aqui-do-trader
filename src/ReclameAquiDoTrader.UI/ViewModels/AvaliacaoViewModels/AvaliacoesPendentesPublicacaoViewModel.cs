using ReclameAquiDoTrader.Business.ValueObjects;
using System;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class AvaliacoesPendentesPublicacaoViewModel
    {
        public string Id { get; set; }
        public string AvaliacaoId { get; set; }
        public DateTime CriadoAs { get; set; }
        public DateTime AtualizadoAs { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Mentor { get; set; }
        public bool Texto { get; set; }
        public bool Respondido { get; set; }
        public bool Positivo { get; set; }
        public bool Expirado { get; set; }
        public bool Publicado { get; set; }
        public RedeSocialList RedesSociais { get; set; }
    }
}
