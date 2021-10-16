using Bogus;
using FluentAssertions;
using ReclameAquiDoTrader.Business.ValueObjects;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class ArquivoListTests
    {
        public ArquivoList arquivos { get; set; }
        public string Nome { get; set; }
        public string ContentType { get; set; }

        public ArquivoListTests()
        {
            arquivos = new ArquivoList();
            Nome = new Faker().Random.String2('a', 'z') + ".jpg";
            ContentType = new Faker().Random.String2('a', 'z');
        }

        [Fact(DisplayName = "Adicionar - Novo Arquivo deve ser adicionado")]
        public void Adicionar_DeveSerAdicionado()
        {
            var arquivo = new Arquivo(Nome, ContentType);
            arquivos.Adicionar(arquivo);

            arquivos.Arquivos.Should().Contain(arquivo);
        }

        [Fact(DisplayName = "Adicionar - Não deve adicionar por já existir valor igual na lista")]
        public void Adicionar_NaoDeveAdicionarValorIgual()
        {
            var arquivo = new Arquivo(Nome, ContentType);
            arquivos.Adicionar(arquivo);

            arquivos.Adicionar(arquivo);

            arquivos.Arquivos.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Limpar arquivos - arquivos deve ser zerado")]
        public void Limpararquivo_ListaDearquivosDeveSerZerado()
        {
            arquivos.LimparArquivos();

            arquivos.Arquivos.Should().BeNullOrEmpty();
        }
    }
}
