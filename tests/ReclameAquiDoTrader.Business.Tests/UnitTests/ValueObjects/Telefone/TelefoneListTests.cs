using FluentAssertions;
using ReclameAquiDoTrader.Business.ValueObjects;
using Xunit;

namespace ReclameAquiDoTrader.Business.Tests.UnitTests.ValueObjects
{
    public class TelefoneListTests
    {
        public TelefoneList Telefones { get; set; }
        public string telefoneParaTeste { get; set; }
        public TelefoneListTests()
        {
            Telefones = new TelefoneList();
            telefoneParaTeste = "(18) 22345-6457";
        }

        [Fact(DisplayName = "Adicionar - Novo Telefone deve ser adicionado")]
        public void Adicionar_DeveSerAdicionada()
        {
            var telefone = new Telefone(telefoneParaTeste);

            Telefones.Adicionar(telefone);

            Telefones.Telefones.Should().Contain(telefone);
        }

        [Fact(DisplayName = "Adicionar - Não deve adicionar por já existir valor igual na lista")]
        public void Adicionar_NaoDeveAdicionarValorIgual()
        {
            var telefone = new Telefone(telefoneParaTeste);
            Telefones.Adicionar(telefone);

            Telefones.Adicionar(telefone);

            Telefones.Telefones.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Limpar Telefones - Telefones deve ser zerado")]
        public void LimparTelefone_ListaDeTelefonesDeveSerLimpado()
        {
            Telefones.LimparTelefones();

            Telefones.Telefones.Should().BeNullOrEmpty();
        }
    }
}
