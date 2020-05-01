using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using ReclameAquiDoTrader.UI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.IntegrationTests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture> { }

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture> { }

    public class IntegrationTestsFixture
    {
        public HttpClient Client;
        public Faker Faker;
        public string AntiForgeryFieldName = "__RequestVerificationToken";
        public string UsuarioExistenteEmail = "teste@teste.com";
        public string UsuarioExistenteSenha = "Teste@123";
        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost:54273"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Faker = new Faker("pt_BR");
            Client = new WebApplicationFactory<Startup>().CreateClient(clientOptions);
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch =
                Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' não encontrado no HTML", nameof(htmlBody));
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
