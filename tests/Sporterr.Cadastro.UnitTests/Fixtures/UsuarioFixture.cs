using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(UsuarioFixtureCollection))]
    public class UsuarioFixtureCollection : ICollectionFixture<UsuarioFixture> { }

    public class UsuarioFixture : IDisposable
    {
        public Usuario CriarUsuarioValido()
        {
            return new Usuario("Andrei", "andreifs@gmail.com", "12345678");
        }

        public Usuario CriarUsuarioInvalido()
        {
            return new Usuario("A", "", "12");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
