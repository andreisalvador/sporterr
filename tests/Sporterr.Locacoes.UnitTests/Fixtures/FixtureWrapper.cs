using System;
using Xunit;

namespace Sporterr.Locacoes.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(FixtureWrapper))]
    public class FixtureWrapperCollection : ICollectionFixture<FixtureWrapper> { }

    public class FixtureWrapper : IDisposable
    {
        private readonly Lazy<SolicitacaoFixture> solicitacaoFixture;
        private readonly Lazy<LocacaoFixture> locacaoFixture;

        public SolicitacaoFixture Solicitacao => solicitacaoFixture.Value;
        public LocacaoFixture Locacao => locacaoFixture.Value;

        public FixtureWrapper()
        {
            solicitacaoFixture = new Lazy<SolicitacaoFixture>();
            locacaoFixture = new Lazy<LocacaoFixture>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
