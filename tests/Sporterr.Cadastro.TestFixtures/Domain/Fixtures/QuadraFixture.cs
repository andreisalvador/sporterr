using Bogus;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    public class QuadraFixture : IDisposable
    {
        public Quadra CriarQuadraValida()
        {
            return new Faker<Quadra>("pt_BR").CustomInstantiator(q => new Quadra(q.PickRandom<TipoEsporte>(), TimeSpan.FromHours(1), q.Random.Decimal(100, 150)));
        }

        public Quadra CriarQuadraInvalida()
        {
            return new Quadra((TipoEsporte)16516, TimeSpan.MinValue, 0);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
