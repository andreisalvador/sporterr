using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(QuadraFixtureCollection))]
    public class QuadraFixtureCollection : ICollectionFixture<QuadraFixture> { }

    public class QuadraFixture : IDisposable
    {
        public Quadra CriarQuadraValida()
        {
            return new Quadra(Core.Enums.Esportes.Futebol, TimeSpan.FromHours(1), 100m);
        }

        public Quadra CriarQuadraInvalida()
        {
            return new Quadra((Esportes)16516, TimeSpan.MinValue, 0);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
