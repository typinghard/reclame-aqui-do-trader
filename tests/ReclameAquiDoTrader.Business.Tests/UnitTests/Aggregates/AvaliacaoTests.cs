using FluentAssertions;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Statics;
using System;
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
            avaliacao.Publicado.Should().BeFalse();
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

        [Fact(DisplayName = "Responder")]
        public void MarcarComoRespondido_DeveMarcarCorretamente()
        {
            avaliacao.Responder(string.Empty);

            avaliacao.Respondido.Should().BeTrue();
        }


        [Fact(DisplayName = "Publicar")]
        public void Publicar_DevePublicarCorretamente()
        {
            avaliacao.Publicar();

            avaliacao.DataPublicacao.Should().HaveValue();
        }

        [Fact(DisplayName = "Remover Publicação")]
        public void RemoverPublicacao_DeveRemoverPublicacaoCorretamente()
        {
            avaliacao.RemoverPublicacao();

            avaliacao.DataPublicacao.Should().NotHaveValue();
        }

        [Fact(DisplayName = "Texto Avaliação")]
        public void Texto_DeveAtribuirNovoTextoCorretamente()
        {
            avaliacao.NovoTexto(string.Empty);

            avaliacao.Texto.Should().NotBeNull();
        }


        [Fact(DisplayName = "Texto Resposta")]
        public void TextoResposta_DeveAtribuirTextoRespostaCorretamente()
        {
            avaliacao.Responder(Texto);

            avaliacao.TextoResposta.Should().NotBeNull();
            avaliacao.DataResposta.Should().HaveValue();
        }


        [Fact(DisplayName = "Remover Texto Resposta")]
        public void RemoverTextoResposta_DeveRemoverTextoRespostaCorretamente()
        {
            avaliacao.RemoverResposta();

            avaliacao.TextoResposta.Should().BeEmpty();
            avaliacao.DataResposta.Should().NotHaveValue();
        }

    }
}
