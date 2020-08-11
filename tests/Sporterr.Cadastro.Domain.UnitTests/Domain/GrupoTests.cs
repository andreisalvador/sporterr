using FluentAssertions;
using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.TestFixtures.Domain;
using Sporterr.Core.DomainObjects.Exceptions;
using System.Linq;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Domain
{
    public class GrupoTests : IClassFixture<DomainFixtures>
    {
        private readonly DomainFixtures _fixtureWrapper;
        public GrupoTests(DomainFixtures fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Novo grupo inválido")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_Validate_GrupoDeveSerInvalido()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.Grupo.Invoking(x => x.CriarGrupoInvalido()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Novo grupo válido")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_Validate_GrupoDeveSerValido()
        {
            //Arrange & Act
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();

            //Assert
            grupo.Should().NotBeNull();
            grupo.Ativo.Should().BeTrue();
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
            novoMembro.Should().BeSameAs(novoGrupo.Membros.SingleOrDefault(m => m.Equals(novoMembro)));
            novoGrupo.QuantidadeMembros.Should().BeGreaterThan(0);
            novoMembro.Id.Should().NotBeEmpty();
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
            novoGrupo.Invoking(x => x.AdicionarMembro(novoMembroDoGrupoCheio)).Should().Throw<DomainException>();
            novoGrupo.QuantidadeMembros.Should().Be((byte)novoGrupo.Membros.Count);
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
            novoGrupo.Invoking(x => x.AdicionarMembro(novoMembro)).Should().Throw<DomainException>();
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
            novoGrupo.Membros.Should().BeEmpty();
            novoGrupo.QuantidadeMembros.Should().Be(0);
        }

        [Fact(DisplayName = "Remove membro que não faz parte do grupo")]
        [Trait("Domain", "Testes do Grupo")]
        public void Grupo_RemoverMembro_DeveFalharPoisMembroNaoFazParteDoGrupo()
        {
            //Arrange
            Grupo novoGrupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            Membro membro = _fixtureWrapper.Membro.CriarMembroValido();

            //Act & Asser
            novoGrupo.Invoking(x => x.RemoverMembro(membro)).Should().Throw<DomainException>();
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
            novoGrupo.GrupoEstaCheio().Should().BeTrue();
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
            novoGrupo.MembroPertenceGrupo(membro).Should().BeTrue();
        }
    }
}
