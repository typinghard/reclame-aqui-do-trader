using System;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.ViewModels.HomeViewModels
{
    public class UltimosMentoresCadastradosViewModel
    {
        public UltimosMentoresCadastradosViewModel()
        {
            Mentores = new List<UltimosMentoresCadastradosItemViewModel>();
        }
        public List<UltimosMentoresCadastradosItemViewModel> Mentores { get; set; }
    }

    public class UltimosMentoresCadastradosItemViewModel
    {
        public DateTime CriadoAs { get; set; }
        public string Nome { get; set; }
        public string Id { get; set; }
    }

}
