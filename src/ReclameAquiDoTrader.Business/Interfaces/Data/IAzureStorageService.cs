using System.Threading.Tasks;

namespace ReclameAquiDoTrader.Business.Interfaces.Data
{
    public interface IAzureStorageService
    {
        Task SalvarArquivo(byte[] byteArray, string nomeArquivo, string containerName);
        byte[] Download(string filename, string containerName);
        void Remover(string filename, string containerName);

        string ObterUri(string filename, string containerName);
    }
}
