using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class DetalhesAvaliacaoViewModel
    {
        public string Id { get; set; }
        public DateTime CriadoAs { get; set; }
        public DateTime AtualizadoAs { get; set; }
        public string Mentor { get; set; }
        public string Texto { get; set; }
        public string Resposta { get; set; }
        public int HorasParaResposta { get; set; }
        public bool Positivo { get; set; }
        public DateTime? DataResposta { get; set; }
        public RedeSocialList RedesSociais { get; set; }
        public bool Expirado { get; set; }
        public DateTime DataExpiracao { get; set; }
        public bool Respondido { get; set; }
        public bool Inativo { get; set; }
        public bool Publicado { get; set; }
        public DateTime? DataPublicacao { get; set; }

        public List<ArquivoViewModel> Comprovantes { get; set; }
    }
}
