using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{

    [CollectionDefinition(nameof(SolicitacaoFixtureCollection))]
    public class SolicitacaoFixtureCollection : ICollectionFixture<SolicitacaoFixture> { }

    public class SolicitacaoFixture : IDisposable
    {
        public Solicitacao CriarSolicitacaoValida()
        {
            return new Solicitacao(Guid.NewGuid(), Guid.NewGuid());
        }

        public Solicitacao CriarSolicitacaoInvalida()
        {
            return new Solicitacao(Guid.Empty, Guid.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
