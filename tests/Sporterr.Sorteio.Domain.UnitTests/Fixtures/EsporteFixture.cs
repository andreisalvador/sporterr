using Bogus;
using Sporterr.Core.Enums;
using Sporterr.Tests.Common.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    public class EsporteFixture : BaseFixture, IDisposable
    {
        public Esporte CriarEsporteValido()
        {
            return NewFakerInstance<Esporte>()
                .CustomInstantiator(e => new Esporte(e.PickRandom<TipoEsporte>().ToString(), e.PickRandom<TipoEsporte>()));
        }

        public Esporte CriarEsporteInvalido()
        {
            return new Esporte(string.Empty, (TipoEsporte)1231231231);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
