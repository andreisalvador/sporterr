using FluentValidation;
using System.Linq;
using Sporterr.Core.Enums;
using Sporterr.Locacoes.Domain;
using Sporterr.Locacoes.UnitTests.Fixtures;
using Xunit;
using Sporterr.Core.DomainObjects.Exceptions;

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
            Assert.NotNull(solicitacao);
        }

        [Fact(DisplayName = "Nova solicitação inválida")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Validate_DeveCriarSolicitacaoInvalida()
        {
            //Arrange & Act & Assert
            Assert.Throws<ValidationException>(() => _fixtureWrapper.Solicitacao.CriarSolicitacaoInvalida());
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
            Assert.Equal(StatusSolicitacao.Aprovada, solicitacao.Status);
            Assert.Equal(1, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.Aprovada));
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
            Assert.Equal(StatusSolicitacao.Cancelada, solicitacao.Status);
            Assert.Equal(1, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.Cancelada));
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
            Assert.Equal(0, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.Cancelada));
            Assert.Equal(StatusSolicitacao.Aprovada, solicitacao.Status);
        }

        [Fact(DisplayName = "Cancelar solicitação que não estava aprovada")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Cancelar_DeveFalharPoisSolicitacaoNaoEstavaAprovada()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();            

            //Act & Assert
            Assert.Throws<DomainException>(() => solicitacao.Cancelar("Motivo cancelamento"));

            //Assert
            Assert.Equal(StatusSolicitacao.AguardandoAprovacao, solicitacao.Status);
            Assert.Equal(0, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.Cancelada));
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
            Assert.Equal(StatusSolicitacao.Recusada, solicitacao.Status);
            Assert.Equal(1, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.Recusada));
        }


        [Fact(DisplayName = "Recusa solicitação sem informar motivo")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_Recusar_DeveFalhaAoRecusarPoisNaoInformouMotivo()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act && Assert
            Assert.Throws<DomainException>(() => solicitacao.Recusar(string.Empty));

            //Assert
            Assert.Equal(StatusSolicitacao.AguardandoAprovacao, solicitacao.Status);
            Assert.Equal(0, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.Recusada));
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
            Assert.Equal(StatusSolicitacao.AguardandoCancelamento, solicitacao.Status);
            Assert.Equal(1, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.AguardandoCancelamento));
        }

        [Fact(DisplayName = "Coloca solicitação que não estava aprovada em aguardo de cancelamento ")]
        [Trait("Domain", "Testes de Solicitação")]
        public void Solicitacao_AguardarCancelamento_DeveFalhaPoisSolicitacaoNaoEstavaAprovada()
        {
            //Arrange
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => solicitacao.AguardarCancelamento());

            //Assert
            Assert.Equal(StatusSolicitacao.AguardandoAprovacao, solicitacao.Status);
            Assert.Equal(0, solicitacao.Historicos.Count(h => h.StatusSolicitacao == StatusSolicitacao.AguardandoCancelamento));
        }
    }
}
