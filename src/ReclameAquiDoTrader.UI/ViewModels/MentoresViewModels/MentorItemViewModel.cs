using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels
{
    public class MentorItemViewModel
    {
        public MentorItemViewModel()
        {
            AreasAtuacaoExistentes = new List<string>();
        }
        public List<string> AreasAtuacaoExistentes { get; set; }
    }
}
