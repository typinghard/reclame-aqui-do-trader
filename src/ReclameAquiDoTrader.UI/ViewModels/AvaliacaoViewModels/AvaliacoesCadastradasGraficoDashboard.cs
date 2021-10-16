using System;

namespace ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels
{
    public class AvaliacoesCadastradasGraficoDashboard
    {
        public DateTime CriadoAs { get; set; }
        public long DataTicks { get; set; }
        public int Qtde { get; set; }
    }

    public class DadosGrafico
    {
        public int Indice { get; set; }
        public string Data { get; set; }
        public DateTime CriadoAs { get; set; }
        public long DataTicks { get; set; }
        public int QtdeAvaliacao { get; set; }
        public int QtdeMentor { get; set; }
        public int QtdeAssinante { get; set; }
    }

}
