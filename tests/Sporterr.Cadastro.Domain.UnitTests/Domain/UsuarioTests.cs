using FluentAssertions;
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
    public class UsuarioTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public UsuarioTests(FixtureWrapper fixturesWrapper)
        {
            _fixtureWrapper = fixturesWrapper;
        }

        [Fact(DisplayName = "Novo usuário inválido")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_Validate_UsuarioDeveInvalido()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.Usuario.Invoking(x => x.CriarUsuarioInvalido()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Novo usuário válido")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_Validate_UsuarioDeveValido()
        {
            //Arrange & Act
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();

            //Assert
            usuario.Should().NotBeNull();
            usuario.Ativo.Should().BeTrue();
        }

        [Fact(DisplayName = "Adicionar nova empresa")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_AdicionarEmpresa_DeveAdicionarEmpresaComSucesso()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();

            //Act
            usuario.AdicionarEmpresa(empresa);

            //Assert
            usuario.Empresas.Should().HaveCount(1);
            empresa.Should().BeSameAs(usuario.Empresas.SingleOrDefault());
        }

        [Fact(DisplayName = "Adicionar empresa já existente")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_AdicionarEmpresa_DeveFalharPorJaExistirEstaEmpresa()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            usuario.AdicionarEmpresa(empresa);

            //Act & Assert
            usuario.Invoking(x => x.AdicionarEmpresa(empresa)).Should().Throw<DomainException>();
        }


        [Fact(DisplayName = "Inativar empresa")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_InativarEmpresa_DeveInativarEmpresa()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            usuario.AdicionarEmpresa(empresa);

            //Act
            usuario.InativarEmpresa(empresa);

            //Assert
            empresa.Ativo.Should().BeFalse();
        }

        [Fact(DisplayName = "Inativar empresa não pertencente ao usuário")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_InativarEmpresa_DeveFalharPelaEmpresaNaoPertencerAoUsuario()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();

            //Act & Assert
            usuario.Invoking(x => x.InativarEmpresa(empresa)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Adicionar novo grupo")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_AdicionarGrupo_DeveAdicionarNovoGrupoAoUsuario()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();

            //Act 
            usuario.AdicionarGrupo(grupo);

            //Assert
            usuario.Grupos.Should().HaveCount(1);
            grupo.Should().BeSameAs(usuario.Grupos.SingleOrDefault());
        }

        [Fact(DisplayName = "Adicionar grupo já existente")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_AdicionarGrupo_DeveFalharPorJaExistirEsteGrupo()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            usuario.AdicionarGrupo(grupo);

            //Act & Assert
            usuario.Invoking(x => x.AdicionarGrupo(grupo)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Remover grupo")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_RemoverGrupo_DeveRemoverGrupoDoUsuario()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();
            usuario.AdicionarGrupo(grupo);

            //Act 
            usuario.RemoverGrupo(grupo);

            //Assert
            usuario.Grupos.Should().BeEmpty();
        }


        [Fact(DisplayName = "Remover grupo não pertencente ao usuário")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_RemoverGrupo_DeveFalhar()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();

            //Act & Assert
            usuario.Invoking(x => x.RemoverGrupo(grupo)).Should().Throw<DomainException>();
        }
    }
}
