using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(GrupoFixtureCollection))]
    public class GrupoFixtureCollection : ICollectionFixture<GrupoFixture> { }

    public class GrupoFixture : IDisposable
    {
        public Grupo CriarGrupoValido(sbyte quantidadeMaximaMembros = 2)
        {
            return new Grupo("Futebol 2020", quantidadeMaximaMembros);
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
