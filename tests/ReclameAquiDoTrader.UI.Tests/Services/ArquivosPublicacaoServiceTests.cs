using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.UI.Services;
using Xunit;

namespace ReclameAquiDoTrader.UI.Tests.Services
{
    public class ArquivosPublicacaoServiceTests
    {
        [Fact(DisplayName = "Mentor - NomeRedeSocial Vazio - Deve Notificar")]
        public void ArquivosPublicacaoService_Mentor_NomeRedeSocialVazio_DeveNotificar()
        {
            var mocker = new AutoMocker();
            var arquivosPublicacaoService = mocker.CreateInstance<ArquivosPublicacaoService>();

            var retorno = arquivosPublicacaoService.Mentor(false, "");

            retorno.Should().BeNull();
            mocker.GetMock<INotificador>().Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact(DisplayName = "Mentor - Deve retornar uma viewmodel")]
        public void ArquivosPublicacaoService_Mentor()
        {
            var mocker = new AutoMocker();
            var arquivosPublicacaoService = mocker.CreateInstance<ArquivosPublicacaoService>();

            var retorno = arquivosPublicacaoService.Mentor(false, "teste");

            retorno.Should().NotBeNull();
            retorno.StyleDinamicoUsuario.Should().NotBeNull();
            retorno.ImagemFundo.Should().NotBeNull();
        }
    }
}
