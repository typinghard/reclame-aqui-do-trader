using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class DetalhesAvaliacaoItemViewModel
    {
        public DetalhesAvaliacaoItemViewModel()
        {
            Avaliacoes = new List<DetalhesAvaliacaoViewModel>();
            AvaliacoesRespostasGrafico = new List<AvaliacoesRespostasGraficoViewModel>();
            AvaliacoesExpiradas = new List<AvaliacoesPendentesPublicacaoViewModel>();
            AvaliacoesEmAndamento = new List<AvaliacoesEmAndamentoViewModel>();
            UltimasAvaliacoesCadastradas = new List<UltimasAvaliacoesCadastradasItemViewModel>();
        }

        public List<DetalhesAvaliacaoViewModel> Avaliacoes { get; set; }
        public List<AvaliacoesRespostasGraficoViewModel> AvaliacoesRespostasGrafico { get; set; }
        public QtantidadeAvaliacoesViewModel QuantidadeAvaliacoes { get; set; }

        public List<AvaliacoesPendentesPublicacaoViewModel> AvaliacoesExpiradas { get; set; }
        public List<AvaliacoesEmAndamentoViewModel> AvaliacoesEmAndamento { get; set; }
        public List<UltimasAvaliacoesCadastradasItemViewModel> UltimasAvaliacoesCadastradas { get; set; }
    }


    public class AvaliacoesRespostasGraficoViewModel
    {
        public bool Resposta { get; set; }
        public int Qtde { get; set; }

    }

    public class QtantidadeAvaliacoesViewModel
    {
        public int Qtde { get; set; }
        public string Texto { get; set; }
        public int QtdeReferenteMesPassado { get; set; }
    }
}
