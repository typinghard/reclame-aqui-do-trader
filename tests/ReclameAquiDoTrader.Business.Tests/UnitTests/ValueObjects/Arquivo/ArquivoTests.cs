using Bogus;
using FluentAssertions;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.ValueObjects;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class ArquivoTests
    {
        public Arquivo Arquivo { get; set; }
        public string Nome { get; set; }
        public string ContentType { get; set; }

        public ArquivoTests()
        {
            Nome = new Faker().Random.String2('a', 'z') + ".jpg";
            ContentType = new Faker().Random.String2('a', 'z');
        }

        [Fact(DisplayName = "ValidarNomeArquivo - Nome do Arquivo deve ser válido")]
        public void ValidarNomeArquivo_DeveSerValido()
        {
            var arquivo = new Arquivo(Nome, ContentType);

            arquivo.Nome.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "ValidarNomeArquivo - Nome do Arquivo deve ser inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abc.")]
        [InlineData("abc")]
        [InlineData(".jpg")]
        public void ValidarNomeArquivo_DeveSerInvalido(string nome)
        {
            var validacao = Assert.Throws<DomainException>(() => new Arquivo(nome, ContentType));

            validacao.Message.Should().Be("Nome de arquivo inválido");
        }
    }
}
