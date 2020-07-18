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
            Assert.Throws<ValidationException>(() => _fixtureWrapper.Usuario.CriarUsuarioInvalido());
        }

        [Fact(DisplayName = "Novo usuário válido")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_Validate_UsuarioDeveValido()
        {
            //Arrange & Act
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();

            //Assert
            Assert.NotNull(usuario);
            Assert.True(usuario.Ativo);
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
            Assert.Equal(1, usuario.Empresas.Count);
            Assert.Same(empresa, usuario.Empresas.SingleOrDefault());
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
            Assert.Throws<DomainException>(() => usuario.AdicionarEmpresa(empresa));
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
            Assert.False(empresa.Ativo);
        }

        [Fact(DisplayName = "Inativar empresa não pertencente ao usuário")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_InativarEmpresa_DeveFalharPelaEmpresaNaoPertencerAoUsuario()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();            

            //Act & Assert
            Assert.Throws<DomainException>(() => usuario.InativarEmpresa(empresa));
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
            Assert.Equal(1, usuario.Grupos.Count);
            Assert.Same(grupo, usuario.Grupos.SingleOrDefault());
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
            Assert.Throws<DomainException>(() => usuario.AdicionarGrupo(grupo));
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
            Assert.Equal(0, usuario.Grupos.Count);
        }


        [Fact(DisplayName = "Remover grupo não pertencente ao usuário")]
        [Trait("Domain", "Testes do Usuário")]
        public void Usuario_RemoverGrupo_DeveFalhar()
        {
            //Arrange
            Usuario usuario = _fixtureWrapper.Usuario.CriarUsuarioValido();
            Grupo grupo = _fixtureWrapper.Grupo.CriarGrupoValido();            

            //Act & Assert
            Assert.Throws<DomainException>(() => usuario.RemoverGrupo(grupo));
        }
    }
}
