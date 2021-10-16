using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.Data.Storage
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly INotificador _notificador;
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        public AzureStorageService(IConfiguration configuration,
                                   INotificador notificador)
        {
            _notificador = notificador;
            _configuration = configuration;
            ConnectionString = _configuration["AzureStorage:ConnectionString"];
        }
        public async Task SalvarArquivo(byte[] byteArray, string nomeArquivo, string containerName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["AzureStorage:ConnectionString"]);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(nomeArquivo);
                await blockBlob.UploadFromStreamAsync(new MemoryStream(byteArray));
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao(string.Empty, $"Tivemos um problema ao gravar suas informações. Por favor, tente novamente em instantes. Erro: {ex.Message}. InnerException: {ex.InnerException?.Message}"));
            }
        }

        public byte[] Download(string filename, string containerName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

                MemoryStream memstream = new MemoryStream();
                Task.Run(() => blockBlob.DownloadToStreamAsync(memstream)).Wait();

                return memstream.ToArray();
            }
            catch (Exception)
            {
                _notificador.Handle(new Notificacao(string.Empty, "Tivemos um problema ao baixar o arquivo. Por favor, tente novamente em instantes"));
                return null;
            }
        }
        public void Remover(string filename, string containerName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

                Task.Run(() => blockBlob.DeleteIfExistsAsync()).Wait();
            }
            catch (Exception)
            {
                _notificador.Handle(new Notificacao(string.Empty, "Tivemos um problema ao remover o arquivo. Por favor, tente novamente em instantes"));
            }
        }

        public string ObterUri(string filename, string containerName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

                return blockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
            }
            catch (Exception)
            {
                _notificador.Handle(new Notificacao(string.Empty, "Tivemos um problema ao obter a url do arquivo. Por favor, tente novamente em instantes"));
                return string.Empty;
            }
        }
    }
}
