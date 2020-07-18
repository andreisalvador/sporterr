using Bogus;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(EsporteFixtureCollection))]
    public class EsporteFixtureCollection : ICollectionFixture<EsporteFixture> { }
    public class EsporteFixture : IDisposable
    {
        public Esporte CriarEsporteValido()
        {
            return new Faker<Esporte>("pt_BR")
                .CustomInstantiator(e => new Esporte(e.PickRandom<TipoEsporte>().ToString(), e.PickRandom<TipoEsporte>()));
        }

        public Esporte CriarEsporteInvalido()
        {
            return new Esporte(string.Empty, (TipoEsporte)1231231231);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
