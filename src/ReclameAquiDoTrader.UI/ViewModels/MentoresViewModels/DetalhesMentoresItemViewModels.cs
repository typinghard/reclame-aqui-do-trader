using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels
{
    public class DetalhesMentoresItemViewModels
    {
        public DetalhesMentoresItemViewModels()
        {
            Mentores = new List<DetalhesMentorViewModels>();
        }
        public List<DetalhesMentorViewModels> Mentores { get; set; }
        public QuantidadeMentoresViewModel QtdeMentores { get; set; }
    }

    public class QuantidadeMentoresViewModel
    {
        public int Qtde { get; set; }
        public string Texto { get; set; }
        public int QtdeReferenteMesPassado { get; set; }
    }
}