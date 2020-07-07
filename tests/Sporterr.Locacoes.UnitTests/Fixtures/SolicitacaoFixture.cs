using Sporterr.Locacoes.Domain;
using System;
using Xunit;

namespace Sporterr.Locacoes.UnitTests.Fixtures
{

    [CollectionDefinition(nameof(SolicitacaoFixtureCollection))]
    public class SolicitacaoFixtureCollection : ICollectionFixture<SolicitacaoFixture> { }

    public class SolicitacaoFixture : IDisposable
    {
        public Solicitacao CriarSolicitacaoValida()
        {
            return new Solicitacao(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new DateTime(2020, 7, 6, 15, 0, 0), new DateTime(2020, 7, 6, 16, 0, 0));
        }

        public Solicitacao CriarSolicitacaoInvalida()
        {
            return new Solicitacao(Guid.Empty, Guid.Empty, Guid.Empty, DateTime.MinValue, DateTime.MinValue);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
