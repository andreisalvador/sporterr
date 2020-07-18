using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(PerfilHabilidadesFixtureCollection))]
    public class PerfilHabilidadesFixtureCollection : ICollectionFixture<PerfilHabilidadesFixture> { }
    public class PerfilHabilidadesFixture : IDisposable
    {
        public PerfilHabilidades CriarPerfilHabilidadeValido()
        {
            return new PerfilHabilidades(Guid.NewGuid());
        }

        public PerfilHabilidades CriarPerfilHabilidadeInvalido()
        {
            return new PerfilHabilidades(Guid.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
