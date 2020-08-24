using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    public class PerfilHabilidadesFixture : IDisposable
    {
        public PerfilHabilidades CriarPerfilHabilidadeValido()
            => new PerfilHabilidades(Guid.NewGuid());

        public PerfilHabilidades CriarPerfilHabilidadeInvalido()
            => new PerfilHabilidades(Guid.Empty);

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
