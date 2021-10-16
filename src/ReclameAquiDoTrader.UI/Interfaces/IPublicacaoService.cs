using ReclameAquiDoTrader.UI.ViewModels.PublicacaoViewModel;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.UI.Interfaces
{
    public interface IPublicacaoService
    {
        PublicacaoMentorViewModel Mentor(bool positivo, string nomeUsuarioRedeSocial);
        List<TextoAvaliacaoViewModel> Avaliacao(bool positivo, string texto);
        List<RespostaMentorViewModel> RespostaMentor(bool positivo, string respostaMentor);
    }
}
