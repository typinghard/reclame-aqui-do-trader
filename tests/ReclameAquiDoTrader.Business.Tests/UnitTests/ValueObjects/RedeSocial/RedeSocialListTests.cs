using Bogus;
using FluentAssertions;
using ReclameAquiDoTrader.Business.Enums;
using ReclameAquiDoTrader.Business.ValueObjects;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class RedeSocialListTests
    {
        public RedeSocialList redesSociais { get; set; }
        public string urlParaTest { get; set; }
        public ERedeSocialTipo ERedeSocialTipo { get; set; }

        public RedeSocialListTests()
        {
            redesSociais = new RedeSocialList();
            urlParaTest = new Faker().Internet.Url();
            ERedeSocialTipo = new Faker().Random.Enum<ERedeSocialTipo>();
        }

        [Fact(DisplayName = "Adicionar - Novo Rede Social deve ser adicionado")]
        public void Adicionar_DeveSerAdicionado()
        {
            var redeSocial = new RedeSocial(urlParaTest, ERedeSocialTipo);
            redeSocial.DefinirUsuario(new Faker().Random.String2('a', 'z'));
            redesSociais.Adicionar(redeSocial);

            redesSociais._redeSocial.Should().Contain(redeSocial);
        }

        [Fact(DisplayName = "Adicionar - Não deve adicionar por já existir valor igual na lista")]
        public void Adicionar_NaoDeveAdicionarValorIgual()
        {
            var redeSocial = new RedeSocial(urlParaTest, ERedeSocialTipo);
            redeSocial.DefinirUsuario(new Faker().Random.String2('a', 'z'));
            redesSociais.Adicionar(redeSocial);

            redesSociais.Adicionar(redeSocial);

            redesSociais._redeSocial.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Limpar Redes Sociais - devem ser zerado")]
        public void LimpaRedeSocial_ListaDeveSerZerada()
        {
            redesSociais.LimparRedesSociais();

            redesSociais._redeSocial.Should().BeNullOrEmpty();
        }
    }
}
