using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.UnitTests.Fixtures;
using Sporterr.Core.DomainObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Domain
{
    [Collection(nameof(FixtureWrapper))]
    public class GrupoTests
    {
        private readonly FixtureWrapper _fixtureWrapper;
        public GrupoTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Nov grupo inválido")]
        [Trait("Domain", "Testes do Grupo")]
        public void Empresa_Validate_GrupoDeveSerInvalido()
        {
            //Arrange & Act & Assert
            Assert.Throws<ValidationException>(() => _fixtureWrapper.Grupo.CriarGrupoInvalido());
        }

        [Fact(DisplayName = "Novo grupo válido")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_Validate_GrupoDeveSerValido()
        {
            //Arrange & Act
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();

            //Assert
            Assert.NotNull(grupo);
            Assert.True(grupo.Ativo);
        }


        [Fact(DisplayName = "Adiciona membro novo ao grupo")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_AdicionarMembro_DeveAdicionarMembroNovoAoGrupo()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro novoMembro = _fixtureWrapper.Membro.CriarMembroValido();

            //Act
            novoGrupo.AdicionarMembro(novoMembro);

            //Assert
            Assert.Same(novoMembro, novoGrupo.Membros.SingleOrDefault(m => m.Equals(novoMembro)));
            Assert.True(novoGrupo.QuantidadeMembros > 0);
            Assert.NotEqual(Guid.Empty, novoMembro.GrupoId);
        }

        [Fact(DisplayName = "Adiciona membro novo em grupo cheio")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_AdicionarMembro_DeveFalharPoisGrupoAtingiuNumeroMaximoDeMembros()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro novoMembro1 = _fixtureWrapper.Membro.CriarMembroValido();
            Membro novoMembro2 = _fixtureWrapper.Membro.CriarMembroValido();
            Membro novoMembroDoGrupoCheio = _fixtureWrapper.Membro.CriarMembroValido();
            novoGrupo.AdicionarMembro(novoMembro1);
            novoGrupo.AdicionarMembro(novoMembro2);

            //Act & Assert
            Assert.Throws<DomainException>(() => novoGrupo.AdicionarMembro(novoMembroDoGrupoCheio));
            Assert.Equal(novoGrupo.QuantidadeMembros, novoGrupo.Membros.Count);
        }

        [Fact(DisplayName = "Adiciona mesmo membro novamente ao grupo")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_AdicionarMembro_DeveFalharPoisMembroJaFazParteDoGrupo()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro novoMembro = _fixtureWrapper.Membro.CriarMembroValido();
            novoGrupo.AdicionarMembro(novoMembro);

            //Act & Assert
            Assert.Throws<DomainException>(() => novoGrupo.AdicionarMembro(novoMembro));
        }

        [Fact(DisplayName = "Remove membro do grupo")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_RemoverMembro_DeveMembroDoGrupo()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro membro = _fixtureWrapper.Membro.CriarMembroValido();
            novoGrupo.AdicionarMembro(membro);

            //Act
            novoGrupo.RemoverMembro(membro);

            //Assert
            Assert.False(novoGrupo.Membros.Any());
            Assert.Equal(0, novoGrupo.QuantidadeMembros);
        }

        [Fact(DisplayName = "Remove membro que não faz parte do grupo")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_RemoverMembro_DeveFalharPoisMembroNaoFazParteDoGrupo()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro membro = _fixtureWrapper.Membro.CriarMembroValido();

            //Act & Asser
            Assert.Throws<DomainException>(() => novoGrupo.RemoverMembro(membro));
        }

        [Fact(DisplayName = "Verifica se o grupo esta cheio")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_GrupoEstaCheio_DeveRetornarQueOGrupoEstaCheio()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro membro1 = _fixtureWrapper.Membro.CriarMembroValido();
            Membro membro2 = _fixtureWrapper.Membro.CriarMembroValido();
            novoGrupo.AdicionarMembro(membro1);
            novoGrupo.AdicionarMembro(membro2);

            //Act & Asser
            Assert.True(novoGrupo.GrupoEstaCheio());
        }

        [Fact(DisplayName = "Verifica se o membro faz parte do grupo")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_GrupoEstaCheio_DeveInformarQueOMembroPertenceAoGrupo()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro membro = _fixtureWrapper.Membro.CriarMembroValido();
            novoGrupo.AdicionarMembro(membro);

            //Act & Asser
            Assert.True(novoGrupo.MembroPertenceGrupo(membro));
        }
    }
}
