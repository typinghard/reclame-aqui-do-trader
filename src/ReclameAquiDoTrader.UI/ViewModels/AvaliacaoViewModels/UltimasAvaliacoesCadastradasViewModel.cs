using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class UltimasAvaliacoesCadastradasViewModel
    {
        public UltimasAvaliacoesCadastradasViewModel()
        {
            Avaliacoes = new List<UltimasAvaliacoesCadastradasItemViewModel>();
        }

        public List<UltimasAvaliacoesCadastradasItemViewModel> Avaliacoes { get; set; }
    }

    public class UltimasAvaliacoesCadastradasItemViewModel
    {
        public string Id { get; set; }
        public string AvaliacaoId { get; set; }
        public DateTime CriadoAs { get; set; }
        public string Mentor { get; set; }
        public bool Respondido { get; set; }
        public bool Positivo { get; set; }
        public RedeSocialList RedesSociais { get; set; }
    }


}
