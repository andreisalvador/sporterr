using Bogus;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
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
