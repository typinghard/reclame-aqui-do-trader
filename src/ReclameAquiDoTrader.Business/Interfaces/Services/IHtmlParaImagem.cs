using ReclameAquiDoTrader.Business.Enums;

namespace ReclameAquiDoTrader.Business.Interfaces.Services
{
    public interface IHtmlParaImagem
    {
        public byte[] FromHtmlString(string html, int width = 1024, EImagemFormato format = EImagemFormato.Jpg, int quality = 100);
    }
}
