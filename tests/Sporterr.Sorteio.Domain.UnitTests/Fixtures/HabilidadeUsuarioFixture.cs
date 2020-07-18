using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(HabilidadeUsuarioFixtureCollection))]
    public class HabilidadeUsuarioFixtureCollection : ICollectionFixture<HabilidadeUsuarioFixture> { }
    public class HabilidadeUsuarioFixture : IDisposable
    {
        public HabilidadeUsuario CriarHabilidadeUsuarioValido()
        {
            return new HabilidadeUsuario(Guid.NewGuid(), Guid.NewGuid());
        }

        public HabilidadeUsuario CriarHabilidadeUsuarioInvalido()
        {
            return new HabilidadeUsuario(Guid.Empty, Guid.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
