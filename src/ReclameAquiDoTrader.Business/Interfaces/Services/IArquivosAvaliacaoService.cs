using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.ValueObjects;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.Business.Interfaces.Services
{
    public interface IArquivosAvaliacaoService
    {
        string ImgNomeMentor(bool positivo, RedeSocial redeSocial);
        List<string> ImgsTexto(bool positivo, string texto, RedeSocial redeSocial);
        List<string> ImgsRespostaMentor(bool positivo, string respostaMentor, RedeSocial redeSocial);
        //byte[] ArquivoPublicacaoZip(Avaliacao avaliacao, RedeSocial instagramMentor, out string nomeArquivoZipado);

        //void RemoverAquivosPublicacao(Avaliacao avaliacao);
    }
}
