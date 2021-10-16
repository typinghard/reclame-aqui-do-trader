using FluentAssertions;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.ValueObjects;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class EmailTests
    {
        public Email email { get; set; }

        [Theory(DisplayName = "Validar - E-mail Deve ser válido")]
        [InlineData("david.jones@proseware.com")]
        [InlineData("d.j@server1.proseware.com")]
        [InlineData("jones@ms1.proseware.com")]
        [InlineData("j@proseware.com9")]
        [InlineData("js#internal@proseware.com")]
        [InlineData("j_9@[129.126.118.1]")]
        [InlineData("js@proseware.com9")]
        [InlineData("j.s@server1.proseware.com")]
        [InlineData("\"j\\\"s\\\"\"@proseware.com")] //"j\"s\""@proseware.com
        [InlineData("someone@somewhere.co.uk")]
        [InlineData("someone+tag@somewhere.net")]
        //[InlineData("futureTLD@somewhere.fooo")]
        //[InlineData("js@contoso.中国")]
        public void Validar_DeveSerValido(string endereco)
        {
            email = new Email(endereco);

            email.Endereco.Should().Be(endereco);
        }


        [Theory(DisplayName = "Validar - E-mail Deve ser inválido")]
        [InlineData("j.@server1.proseware.com")]
        [InlineData("j..s@proseware.com")]
        [InlineData("js*@proseware.com")]
        [InlineData("js@proseware..com")]
        [InlineData("fdsa")]
        [InlineData("fdsa@")]
        [InlineData("fdsa@fdsa")]
        [InlineData("fdsa@fdsa.")]
        public void Validar_DeveSerInvalido(string endereco)
        {
            var validacao = Assert.Throws<DomainException>(() => new Email(endereco));

            validacao.Message.Should().Be("Email é inválido");
        }
    }
}
