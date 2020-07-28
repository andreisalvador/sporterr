using FluentValidation;
using System.Linq;
using Sporterr.Core.Enums;
using Sporterr.Locacoes.Domain;
using Sporterr.Locacoes.UnitTests.Fixtures;
using Xunit;
using Sporterr.Core.DomainObjects.Exceptions;
using FluentAssertions;

namespace Sporterr.Locacoes.UnitTests.Domain
{
    [Collection(nameof(FixtureWrapper))]
    public class SolicitacaoTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public SolicitacaoTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Nova solicitação válida")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Validate_DeveCriarSolicitacaoValida()
        {
            //Arrange & Act
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Assert
            solicitacao.Should().NotBeNull();
        }

        [Fact(DisplayName = "Nova solicitação inválida")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Validate_DeveCriarSolicitacaoInvalida()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.Solicitacao.Invoking(x => x.CriarSolicitacaoInvalida()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Aprova solicitação")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Aprovar_DeveAprovarSolicitacao()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act
            solicitacao.Aprovar();

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.Aprovada);
            solicitacao.Historicos.Should().Contain(h => h.StatusSolicitacao == StatusSolicitacao.Aprovada);
        }

        [Fact(DisplayName = "Cancelar solicitação")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Cancelar_DeveCancelarSolicitacao()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            solicitacao.Aprovar();

            //Act
            solicitacao.Cancelar("Motivo cancelamento");

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.Cancelada);
            solicitacao.Historicos.Should().Contain(h => h.StatusSolicitacao == StatusSolicitacao.Aprovada);
        }


        [Fact(DisplayName = "Cancelar solicitação sem informar motivo")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Cancelar_DeveFalhaAoCancelarPoisNaoInformouMotivo()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            solicitacao.Aprovar();

            //Act && Assert
            Assert.Throws<DomainException>(() => solicitacao.Cancelar(string.Empty));

            //Assert
            solicitacao.Historicos.Should().NotContain(h => h.StatusSolicitacao == StatusSolicitacao.Cancelada);
            solicitacao.Status.Should().Be(StatusSolicitacao.Aprovada);
        }

        [Fact(DisplayName = "Cancelar solicitação que não estava aprovada")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Cancelar_DeveFalharPoisSolicitacaoNaoEstavaAprovada()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act & Assert
            solicitacao.Invoking(x => x.Cancelar("Motivo cancelamento")).Should().Throw<DomainException>();

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.AguardandoAprovacao);
            solicitacao.Historicos.Should().NotContain(h => h.StatusSolicitacao == StatusSolicitacao.Cancelada);
        }

        [Fact(DisplayName = "Recusar solicitação")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Recusar_DeveRecusarSolicitacao()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();


            //Act
            solicitacao.Recusar("Motivo cancelamento");

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.Recusada);
            solicitacao.Historicos.Should().Contain(h => h.StatusSolicitacao == StatusSolicitacao.Recusada);
        }


        [Fact(DisplayName = "Recusa solicitação sem informar motivo")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Recusar_DeveFalhaAoRecusarPoisNaoInformouMotivo()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act && Assert
            solicitacao.Invoking(x => x.Recusar(string.Empty)).Should().Throw<DomainException>();

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.AguardandoAprovacao);
            solicitacao.Historicos.Should().NotContain(h => h.StatusSolicitacao == StatusSolicitacao.Recusada);
        }

        [Fact(DisplayName = "Coloca solicitação em aguardo de cancelamento")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_AguardarCancelamento_DevePorSolicitacaoAguardoDeCancelamento()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            solicitacao.Aprovar();

            //Act
            solicitacao.AguardarCancelamento();

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.AguardandoCancelamento);
            solicitacao.Historicos.Should().Contain(h => h.StatusSolicitacao == StatusSolicitacao.AguardandoCancelamento);
        }

        [Fact(DisplayName = "Coloca solicitação que não estava aprovada em aguardo de cancelamento ")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_AguardarCancelamento_DeveFalhaPoisSolicitacaoNaoEstavaAprovada()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act & Assert
            solicitacao.Invoking(x => x.AguardarCancelamento()).Should().Throw<DomainException>();

            //Assert
            solicitacao.Status.Should().Be(StatusSolicitacao.AguardandoAprovacao);
            solicitacao.Historicos.Should().NotContain(h => h.StatusSolicitacao == StatusSolicitacao.AguardandoCancelamento);
        }
    }
}
