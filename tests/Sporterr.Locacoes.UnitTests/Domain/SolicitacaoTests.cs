using FluentValidation;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Locacoes.Domain;
using Sporterr.Locacoes.UnitTests.Fixtures;
using Xunit;

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
    }
}
