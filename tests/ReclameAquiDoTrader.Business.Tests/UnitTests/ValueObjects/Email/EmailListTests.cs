using FluentAssertions;
using ReclameAquiDoTrader.Business.ValueObjects;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class EmailListTests
    {
        public EmailList emails { get; set; }
        public string enderecoParaTeste { get; set; }
        public EmailListTests()
        {
            emails = new EmailList();
            enderecoParaTeste = "teste@teste.com";
        }

        [Fact(DisplayName = "Adicionar - Novo E-mail deve ser adicionado")]
        public void Adicionar_DeveSerAdicionado()
        {
            var email = new Email(enderecoParaTeste);
            emails.Adicionar(email);

            emails.Emails.Should().Contain(email);
        }

        [Fact(DisplayName = "Adicionar - Não deve adicionar por já existir valor igual na lista")]
        public void Adicionar_NaoDeveAdicionarValorIgual()
        {
            var email = new Email(enderecoParaTeste);
            emails.Adicionar(email);

            emails.Adicionar(email);

            emails.Emails.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Limpar emails - emails deve ser zerado")]
        public void Limparemail_ListaDeEmailsDeveSerZerado()
        {
            emails.LimparEmails();

            emails.Emails.Should().BeNullOrEmpty();
        }

    }
}
