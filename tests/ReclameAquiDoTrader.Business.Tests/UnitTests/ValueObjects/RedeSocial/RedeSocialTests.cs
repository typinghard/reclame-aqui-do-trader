using FluentAssertions;
using ReclameAquiDoTrader.Business.ValueObjects;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class RedeSocialTests
    {
        public RedeSocial redeSocial { get; set; }
        public RedeSocialTests()
        {
            redeSocial = new RedeSocial("urlteste.com", Enums.ERedeSocialTipo.INSTAGRAM);
        }

        public void DefinirUsuario_DeveSerDefinido(string usuario)
        {
            redeSocial.DefinirUsuario(usuario);

            redeSocial.Usuario.Should().Be(usuario);
        }

    }
}
