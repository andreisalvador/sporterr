using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using Sporterr.Tests.Common.Fixtures;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    public class GrupoFixture : BaseFixture , IDisposable
    {
        public Grupo CriarGrupoValido(byte quantidadeMaximaMembros = 2)
        {
            return NewFakerInstance<Grupo>().CustomInstantiator(g => new Grupo($"Grupo de {g.PickRandom<TipoEsporte>()}", quantidadeMaximaMembros));
        }

        public Grupo CriarGrupoInvalido()
        {
            return new Grupo(string.Empty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
