using FluentAssertions;
using Moq;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Cadastro.Application.Commands.Handlers;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using Sporterr.Cadastro.TestFixtures.Application;
using Sporterr.Cadastro.TestFixtures.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sporterr.Cadastro.Application.UnitTests.Handlers
{
    public class UsuarioCommandHandlerTests : IClassFixture<ApplicationFixtures>, IClassFixture<DomainFixtures>
    {
        private readonly ApplicationFixtures _applicationFixtures;
        private readonly DomainFixtures _domainFixtures;
        private readonly UsuarioCommandHandler _usuarioCommandHandler;

        public UsuarioCommandHandlerTests(ApplicationFixtures applicationFixtures, DomainFixtures domainFixtures)
        {
            _applicationFixtures = applicationFixtures;
            _domainFixtures = domainFixtures;
            _usuarioCommandHandler = _applicationFixtures.ObterCommandHandler<UsuarioCommandHandler>();
        }

        [Fact(DisplayName = "Adiciona usuário")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarUsuarioCommand_Handle_DeveAdicionarUsuario()
        {
            //Arrange
            AdicionarUsuarioCommand adicionarUsuarioCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarUsuarioCommandValido();
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _usuarioCommandHandler.Handle(adicionarUsuarioCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AdicionarUsuario(It.IsAny<Usuario>()), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Nao deve adicionar usuário")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarUsuarioCommand_Handle_NaoDeveAdicionarUsuarioPorCommandInvalido()
        {
            //Arrange
            AdicionarUsuarioCommand adicionarUsuarioCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarUsuarioCommandInvalido();

            //Act
            await _usuarioCommandHandler.Handle(adicionarUsuarioCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AdicionarUsuario(It.IsAny<Usuario>()), Times.Never);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Never);
        }

        [Fact(DisplayName = "Adiciona empresa")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarEmpresaCommand_Handle_DeveAdicionarEmpresa()
        {
            //Arrange
            AdicionarEmpresaCommand adicionarEmpresaCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarEmpresaCommandValido();
            Usuario usuario = _domainFixtures.Usuario.CriarUsuarioValido();
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(adicionarEmpresaCommand.UsuarioProprietarioId)).ReturnsAsync(usuario);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _usuarioCommandHandler.Handle(adicionarEmpresaCommand, ApplicationFixtures.CancellationToken);

            //Assert
            usuario.Empresas.Should().HaveCount(1);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AdicionarEmpresa(It.IsAny<Empresa>()), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }


        [Fact(DisplayName = "Não adicionar empresa")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarEmpresaCommand_Handle_NaoDeveAdicionarEmpresaSemEncontrarUsuario()
        {
            //Arrange
            AdicionarEmpresaCommand adicionarEmpresaCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarEmpresaCommandValido();                       

            //Act
            await _usuarioCommandHandler.Handle(adicionarEmpresaCommand, ApplicationFixtures.CancellationToken);

            //Assert            
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AdicionarEmpresa(It.IsAny<Empresa>()), Times.Never);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Adiciona grupo")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarGrupoCommand_Handle_DeveAdicionarGrupo()
        {
            //Arrange
            AdicionarGrupoCommand adicionarGrupoCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarGrupoCommandValido();
            Usuario usuario = _domainFixtures.Usuario.CriarUsuarioValido();
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(adicionarGrupoCommand.UsuarioCriadorId)).ReturnsAsync(usuario);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _usuarioCommandHandler.Handle(adicionarGrupoCommand, ApplicationFixtures.CancellationToken);

            //Assert
            usuario.Grupos.Should().HaveCount(1);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AdicionarGrupo(It.IsAny<Grupo>()), Times.Once);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AtualizarUsuario(usuario), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Não adicionar grupo")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarEmpresaCommand_Handle_NaoDeveAdicionarGrupoSemEncontrarUsuario()
        {
            //Arrange
            AdicionarGrupoCommand adicionarGrupoCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarGrupoCommandValido();

            //Act
            await _usuarioCommandHandler.Handle(adicionarGrupoCommand, ApplicationFixtures.CancellationToken);

            //Assert            
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AdicionarGrupo(It.IsAny<Grupo>()), Times.Never);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Inativa empresa")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_InativarEmpresaCommand_Handle_DeveInativarEmpresa()
        {
            //Arrange
            InativarEmpresaCommand inativarEmpresaCommand = _applicationFixtures.UsuarioCommandHandler.CriarInvativarEmpresaCommandValido();
            Empresa empresa = _domainFixtures.Empresa.CriarEmpresaValida();
            Usuario usuario = _domainFixtures.Usuario.CriarUsuarioValido();
            usuario.AdicionarEmpresa(empresa);

            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(inativarEmpresaCommand.UsuarioProprietarioEmpresaId)).ReturnsAsync(usuario);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterEmpresaPorId(inativarEmpresaCommand.EmpresaId)).ReturnsAsync(empresa);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _usuarioCommandHandler.Handle(inativarEmpresaCommand, ApplicationFixtures.CancellationToken);

            //Assert
            empresa.Ativo.Should().BeFalse();
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AtualizarEmpresa(empresa), Times.Once);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AtualizarUsuario(usuario), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Não inativa empresa")]
        [Trait("Application", "Testes Usuario Command Handler")]
        public async Task UsuarioCommandHandler_AdicionarEmpresaCommand_Handle_NaoDeveInativarEmpresaSemEncontrarUsuarioOuEmpresa()
        {
            //Arrange
            AdicionarGrupoCommand adicionarGrupoCommand = _applicationFixtures.UsuarioCommandHandler.CriarAdicionarGrupoCommandValido();
            Empresa empresa = _domainFixtures.Empresa.CriarEmpresaValida();
            Usuario usuario = _domainFixtures.Usuario.CriarUsuarioValido();
            usuario.AdicionarEmpresa(empresa);

            //Act
            await _usuarioCommandHandler.Handle(adicionarGrupoCommand, ApplicationFixtures.CancellationToken);

            //Assert            
            empresa.Ativo.Should().BeTrue();
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AtualizarEmpresa(empresa), Times.Never);
            _applicationFixtures.Mocker.GetMock<IUsuarioRepository>().Verify(r => r.AtualizarUsuario(usuario), Times.Never);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }
    }
}
