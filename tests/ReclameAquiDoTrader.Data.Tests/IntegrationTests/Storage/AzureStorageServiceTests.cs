using Microsoft.Extensions.Configuration;
using Moq;
using Moq.AutoMock;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Data.Storage;
using System.Threading.Tasks;
using Xunit;

namespace ReclameAquiDoTrader.Data.Tests.IntegrationTests.Storage
{
    public class AzureStorageServiceTests
    {
        private AutoMocker Mocker;
        private AzureStorageService azureStorageService;
        public AzureStorageServiceTests()
        {
            Mocker = new AutoMocker();
            azureStorageService = Mocker.CreateInstance<AzureStorageService>(true);
        }
        [Fact(DisplayName = "Salvar")]
        public async Task AzureStorageService_Salvar()
        {
            Mocker.GetMock<IConfiguration>().Setup(r => r["AzureStorage:ConnectionString"])
                                            .Returns("DefaultEndpointsProtocol=https;AccountName=radtstorage;AccountKey=kvZ/S8EYfjTOW2sCPGSx21R7T30JM2H0ig8ad4fRwhc/a2blXTGzInEqdia040O7pJjvvYdqJWyvJWIo7RJeKA==;EndpointSuffix=core.windows.net");
            var bytes = new byte[1] { 1 };

            await azureStorageService.SalvarArquivo(bytes, "teste.txt", "comprovantes");

            Mocker.GetMock<INotificador>().Verify(x => x.Handle(It.IsAny<Notificacao>()), Times.Never);
        }
    }
}
