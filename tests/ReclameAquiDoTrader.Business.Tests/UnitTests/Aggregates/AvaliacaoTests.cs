using FluentAssertions;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Statics;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.Aggregates
{
    public class AvaliacaoTests
    {
        private Avaliacao avaliacao;
        private string MentorId = Guid.NewGuid().ToString();
        private string Texto = "Texto para teste";
        public AvaliacaoTests() 
        {
            avaliacao = new Avaliacao(MentorId, Texto);
        }

        [Fact(DisplayName = "Construtor deve inicializar corretamente")]
        public void DeveInicializarCorretamente()
        {
            avaliacao.MentorId.Should().Be(MentorId);
            avaliacao.Texto.Should().Be(Texto);
            avaliacao.HorasParaResposta.Should().Be(AvaliacaoStatics.TEMPO_EM_HORAS_PARA_EXPIRACAO);
            avaliacao.Expirado.Should().BeFalse();
            avaliacao.Respondido.Should().BeFalse();
            avaliacao.Positivo.Should().BeFalse();
            avaliacao.DataExpiracao.Should().Be(avaliacao.CriadoAs.AddHours(AvaliacaoStatics.TEMPO_EM_HORAS_PARA_EXPIRACAO));
        }

        [Fact(DisplayName = "Positivar")]
        public void Positivar_DevePositivarCorretamente()
        {
            avaliacao.Positivar();

            avaliacao.Positivo.Should().BeTrue();
        }

        [Fact(DisplayName = "Negativar")]
        public void Negativar_DeveNegativarCorretamente()
        {
            avaliacao.Negativar();

            avaliacao.Positivo.Should().BeFalse();
        }

        [Fact(DisplayName = "Marcar como respondido")]
        public void MarcarComoRespondido_DeveMarcarCorretamente()
        {
            avaliacao.MarcarComoRespondido();

            avaliacao.Respondido.Should().BeTrue();
        }

        [Fact(DisplayName = "AdicionarTag - Nova tag deve ser adicionada")]
        public void AdicionarTag_NovaTagDeveSerAdicionada()
        {
            avaliacao.AdicionarTag("tag 1");

            avaliacao.Tags.Should().Contain("tag 1");
        }

        [Theory(DisplayName = "AdicionarTag - Tag repetida não deve ser adicionada")]
        [InlineData("tag 1", "tag 1")]
        [InlineData(" Tag 1 ", " taG 1")]
        public void AdicionarTag_TagRepetidaNaoDeveSerAdicionada(string tag, string tagRepetida)
        {
            avaliacao.AdicionarTag(tag);
            avaliacao.AdicionarTag(tagRepetida);

            avaliacao.Tags.Should().ContainSingle(tag);
        }

        [Theory(DisplayName = "AdicionarTag - Tag vazia deve lançar exceção")]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void AdicionarTag_TagVaziaLancaExcecao(string tag)
        {
            Assert.Throws<DomainException>(() => avaliacao.AdicionarTag(tag));
        }
    }
}
