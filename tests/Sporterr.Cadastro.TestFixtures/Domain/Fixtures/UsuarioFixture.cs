﻿using Bogus;
using Sporterr.Cadastro.Domain;
using System;
using static Bogus.DataSets.Name;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    public class UsuarioFixture : IDisposable
    {
        public Usuario CriarUsuarioValido()
        {
            return new Faker<Usuario>("pt_BR")
                .CustomInstantiator(u => new Usuario(u.Name.FirstName(u.PickRandom<Gender>()), u.Internet.Email(), u.Internet.Password(20)));
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
