using Sporterr.Cadastro.Domain;
using Sporterr.Tests.Common.Fixtures;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    public class UsuarioFixture : BaseFixture, IDisposable
    {
        public Usuario CriarUsuarioValido()
        {
            return NewFakerInstance<Usuario>()
                .CustomInstantiator(u => new Usuario(u.Name.FirstName(u.PickRandom<Bogus.DataSets.Name.Gender>()), u.Internet.Email(), u.Internet.Password(20)));
        }

        public Usuario CriarUsuarioInvalido()
        {
            return new Usuario("A", "", "12");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
