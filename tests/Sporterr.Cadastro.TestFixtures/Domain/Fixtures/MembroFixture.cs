using Sporterr.Cadastro.Domain;
using System;
using Xunit;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    [CollectionDefinition(nameof(MembroFixtureCollection))]
    public class MembroFixtureCollection : ICollectionFixture<MembroFixture> { }

    public class MembroFixture : IDisposable
    {
        public Membro CriarMembroValido()
        {
            return new Membro(Guid.NewGuid());
        }

        public Membro CriarMembroInvalido()
        {
            return new Membro(Guid.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}