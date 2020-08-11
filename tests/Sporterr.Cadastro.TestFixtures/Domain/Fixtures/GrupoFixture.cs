using Bogus;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    [CollectionDefinition(nameof(GrupoFixtureCollection))]
    public class GrupoFixtureCollection : ICollectionFixture<GrupoFixture> { }

    public class GrupoFixture : IDisposable
    {
        public Grupo CriarGrupoValido(byte quantidadeMaximaMembros = 2)
        {
            return new Faker<Grupo>("pt_BR").CustomInstantiator(g => new Grupo($"Grupo de {g.PickRandom<TipoEsporte>()}", quantidadeMaximaMembros));
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
