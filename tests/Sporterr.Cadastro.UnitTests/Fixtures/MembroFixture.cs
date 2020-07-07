using Sporterr.Cadastro.Domain;
using System;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(MembroFixtureCollection))]
    public class MembroFixtureCollection : ICollectionFixture<MembroFixture> { }

    public class MembroFixture
    {
        public Membro CriarMembroValido()
        {
            return new Membro(Guid.NewGuid());
        }

        public Membro CriarMembroInvalido()
        {
            return new Membro(Guid.Empty);
        }
    }
}