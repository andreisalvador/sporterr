using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(HabilidadeFixtureCollection))]
    public class HabilidadeFixtureCollection : ICollectionFixture<HabilidadeFixture> { }
    public class HabilidadeFixture : IDisposable
    {
        public Habilidade CriarHabilidadeValida()
        {
            return new Faker<Habilidade>("pt_BR")
                                        .CustomInstantiator(h => new Habilidade(h.Random.String()));
        }


        public Habilidade CriarHabilidadeInvalida()
        {
            return new Habilidade(string.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
