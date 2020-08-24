using Sporterr.Tests.Common.Fixtures;
using System;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    public class HabilidadeFixture : BaseFixture, IDisposable
    {
        public Habilidade CriarHabilidadeValida()
            => NewFakerInstance<Habilidade>().CustomInstantiator(h => new Habilidade(h.Random.String()));

        public Habilidade CriarHabilidadeInvalida()
            => new Habilidade(string.Empty);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
