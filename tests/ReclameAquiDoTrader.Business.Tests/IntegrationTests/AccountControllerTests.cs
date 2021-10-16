using FluentAssertions;
using ReclameAquiDoTrader.Business.Tests.IntegrationTests.Config;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.IntegrationTests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class AccountControllerTests
    {
        private readonly IntegrationTestsFixture _testsFixture;

        public AccountControllerTests(IntegrationTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Login valido")]
        public async Task Login()
        {
            // Arrange
            var initialResponse = await _testsFixture.Client.GetAsync("/Account/Login");
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                {_testsFixture.AntiForgeryFieldName, antiForgeryToken},
                {"Email", _testsFixture.UsuarioExistenteEmail},
                {"Password", _testsFixture.UsuarioExistenteSenha}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Account/Login")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Login inválido")]
        public async Task Login_Invalido()
        {
            // Arrange
            var initialResponse = await _testsFixture.Client.GetAsync("/Account/Login");
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                {_testsFixture.AntiForgeryFieldName, antiForgeryToken},
                {"Email", "nao@existe.com"},
                {"Password", "errado"}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Account/Login")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseString.Should().Contain("Usuário ou Senha incorretos");
        }
    }
}
