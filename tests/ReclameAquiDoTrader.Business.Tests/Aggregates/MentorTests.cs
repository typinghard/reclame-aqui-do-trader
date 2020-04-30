using FluentAssertions;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.Aggregates
{
    public class MentorTests
    {
        private Mentor mentor;
        public MentorTests()
        {
            mentor = new Mentor("Mentor 1");
        }
        [Fact(DisplayName = "Atribuir site")]
        public void AtribuirSite_DeveAtribuirCorretamente()
        {
            mentor.AtribuirSite("www.foo.com");

            mentor.Site.Should().Be("www.foo.com");
        }

        [Fact(DisplayName = "AdicionarAreaDeAtuacao - Nova AreaDeAtuacao deve ser adicionada")]
        public void AdicionarAreaDeAtuacao_NovaAreaDeAtuacaoDeveSerAdicionada()
        {
            mentor.AdicionarAreaDeAtuacao("AreaDeAtuacao 1");

            mentor.AreasDeAtuacao.Should().Contain("AreaDeAtuacao 1");
        }

        [Theory(DisplayName = "AdicionarAreaDeAtuacao - AreaDeAtuacao repetida não deve ser adicionada")]
        [InlineData("AreaDeAtuacao 1", "AreaDeAtuacao 1")]
        [InlineData(" AreaDeAtuacao 1 ", " AreaDeAtuacao 1")]
        public void AdicionarAreaDeAtuacao_AreaDeAtuacaoRepetidaNaoDeveSerAdicionada(string AreaDeAtuacao, string AreaDeAtuacaoRepetida)
        {
            mentor.AdicionarAreaDeAtuacao(AreaDeAtuacao);
            mentor.AdicionarAreaDeAtuacao(AreaDeAtuacaoRepetida);

            mentor.AreasDeAtuacao.Should().ContainSingle(AreaDeAtuacao);
        }

        [Theory(DisplayName = "AdicionarAreaDeAtuacao - AreaDeAtuacao vazia deve lançar exceção")]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void AdicionarAreaDeAtuacao_AreaDeAtuacaoVaziaLancaExcecao(string AreaDeAtuacao)
        {
            Assert.Throws<DomainException>(() => mentor.AdicionarAreaDeAtuacao(AreaDeAtuacao));
        }
    }
}
