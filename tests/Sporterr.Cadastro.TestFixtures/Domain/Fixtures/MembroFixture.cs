using Sporterr.Cadastro.Domain;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
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