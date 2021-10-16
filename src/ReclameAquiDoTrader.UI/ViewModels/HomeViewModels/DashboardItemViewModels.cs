using ReclameAquiDoTrader.UI.ViewModels.AssinantesViewModels;
using ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.HomeViewModels
{
    public class DashboardItemViewModels
    {
        public DashboardItemViewModels()
        {
            Avaliacoes = new List<DetalhesAvaliacaoViewModel>();
            AvaliacoesRespostasGrafico = new List<AvaliacoesRespostasGraficoViewModel>();
            AvaliacoesPendentesPublicacao = new List<AvaliacoesPendentesPublicacaoViewModel>();
            AvaliacoesEmAndamento = new List<AvaliacoesEmAndamentoViewModel>();
            UltimasAvaliacoesCadastradas = new List<UltimasAvaliacoesCadastradasItemViewModel>();
            UltimosMentoresCadastrados = new List<UltimosMentoresCadastradosItemViewModel>();

            DadosGrafico = new List<DadosGrafico>();


        }

        public int QtdeTotalMentores { get; set; }
        public int QtdeTotalAvaliacoes { get; set; }
        public int QtdeTotalAssinantes { get; set; }
        public int QtdeAvaliacoesPendentesPublicacao { get; set; }
        public int QtdeAvaliacoesEmAndamento { get; set; }

        public List<DetalhesAvaliacaoViewModel> Avaliacoes { get; set; }
        public List<AvaliacoesRespostasGraficoViewModel> AvaliacoesRespostasGrafico { get; set; }
        public QtantidadeAvaliacoesViewModel QuantidadeAvaliacoes { get; set; }

        public List<AvaliacoesPendentesPublicacaoViewModel> AvaliacoesPendentesPublicacao { get; set; }
        public List<AvaliacoesEmAndamentoViewModel> AvaliacoesEmAndamento { get; set; }
        public List<UltimasAvaliacoesCadastradasItemViewModel> UltimasAvaliacoesCadastradas { get; set; }
        public List<UltimosMentoresCadastradosItemViewModel> UltimosMentoresCadastrados { get; set; }
        public List<UltimosAssinantesCadastradosViewModel> UltimosAssinantesCadastrados { get; set; }

        public List<DadosGrafico> DadosGrafico { get; set; }
    }

}
