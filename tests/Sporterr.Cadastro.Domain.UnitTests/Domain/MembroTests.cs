using FluentAssertions;
using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Domain
{
    [Collection(nameof(FixtureWrapper))]
    public class MembroTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public MembroTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Novo membro inválido")]
        [Trait("Domain", "Testes do Membro")]
        public void Membro_Validate_MembroDeveSerInvalido()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.Membro.Invoking(x => x.CriarMembroInvalido()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Novo membro válido")]
        [Trait("Domain", "Testes do Membro")]
        public void Membro_Validate_MembroDeveSerValido()
        {
            //Arrange & Act
            Membro membro = _fixtureWrapper.Membro.CriarMembroValido();

            //Assert
            membro.Should().NotBeNull();
        }
    }
}
