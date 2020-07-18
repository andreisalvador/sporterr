using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(AvaliacaoHabilidadeFixtureCollection))]
    public class AvaliacaoHabilidadeFixtureCollection : ICollectionFixture<AvaliacaoHabilidadeFixture> { }
    public class AvaliacaoHabilidadeFixture : IDisposable
    {
        public AvaliacaoHabilidade CriarAvaliacaoHabilidadeValida()
        {
            return new Faker<AvaliacaoHabilidade>("pt_BR")
                .CustomInstantiator(a => new AvaliacaoHabilidade(a.Random.Guid(), a.Random.Double(0, 10)));
        }

        public AvaliacaoHabilidade CriarAvaliacaoHabilidadeInvalida()
        {
            return new AvaliacaoHabilidade(Guid.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
