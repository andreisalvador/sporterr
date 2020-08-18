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
    public class GrupoCommandHandlerTests : IClassFixture<ApplicationFixtures>, IClassFixture<DomainFixtures>
    {
        private readonly ApplicationFixtures _applicationFixtures;
        private readonly DomainFixtures _domainFixtures;
        private readonly GrupoCommandHandler _grupoCommandHandler;

        public GrupoCommandHandlerTests(ApplicationFixtures applicationFixtures, DomainFixtures domainFixtures)
        {
            _applicationFixtures = applicationFixtures;
            _domainFixtures = domainFixtures;
            _grupoCommandHandler = _applicationFixtures.ObterCommandHandler<GrupoCommandHandler>();
        }

        [Fact(DisplayName = "Adiciona novo membro ao grupo e dispara evento")]
        [Trait("Application", "Testes Grupo Command Handler")]
        public async Task GrupoCommandHandler_Handle_AdicionarMembroCommand_DeveAdicionarNovoMembroAoGrupo()
        {
            //Arrange             
            AdicionarMembroCommand adicionarMembroCommand = _applicationFixtures.GrupoCommandHandler.CriarAdicionarMembroCommandValido();
            Grupo grupo = _domainFixtures.Grupo.CriarGrupoValido();
            _applicationFixtures.Mocker.GetMock<IGrupoRepository>().Setup(r => r.ObterGrupoPorId(adicionarMembroCommand.GrupoId)).ReturnsAsync(grupo);
            _applicationFixtures.Mocker.GetMock<IGrupoRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _grupoCommandHandler.Handle(adicionarMembroCommand, ApplicationFixtures.CancellationToken);

            //Arrange
            grupo.Membros.Should().HaveCount(1);
            _applicationFixtures.Mocker.Verify<IGrupoRepository>(r => r.AdicionarMembro(grupo.Membros.SingleOrDefault()), Times.Once);
            _applicationFixtures.Mocker.Verify<IGrupoRepository>(r => r.AtualizarGrupo(grupo), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Nao adiciona novo membro ao grupo e dispara evento de falha")]
        [Trait("Application", "Testes Grupo Command Handler")]
        public async Task GrupoCommandHandler_Handle_AdicionarMembroCommand_NaoDeveAdicionarNovoMembroAoGrupo()
        {
            //Arrange             
            AdicionarMembroCommand adicionarMembroCommand = _applicationFixtures.GrupoCommandHandler.CriarAdicionarMembroCommandValido();

            //Act
            await _grupoCommandHandler.Handle(adicionarMembroCommand, ApplicationFixtures.CancellationToken);

            //Arrange
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Remove membro do grupo e dispara evento")]
        [Trait("Application", "Testes Grupo Command Handler")]
        public async Task GrupoCommandHandler_Handle_RemoverMembroGrupoCommand_DeveRemoverMembroDoGrupo() 
        {
            //Arrange
            RemoverMembroGrupoCommand removerMembroGrupoCommand = _applicationFixtures.GrupoCommandHandler.CriarRemoverMembroGrupoCommandValido();
            Membro membro = _domainFixtures.Membro.CriarMembroValido();
            Grupo grupo = _domainFixtures.Grupo.CriarGrupoValido();

            Mock<IGrupoRepository> grupoRepositoryMock = _applicationFixtures.Mocker.GetMock<IGrupoRepository>();
            grupoRepositoryMock.Setup(r => r.ObterMembroPorId(removerMembroGrupoCommand.MembroId)).ReturnsAsync(membro);
            grupoRepositoryMock.Setup(r => r.ObterGrupoPorId(removerMembroGrupoCommand.GrupoId)).ReturnsAsync(grupo);
            grupoRepositoryMock.Setup(r => r.Commit()).ReturnsAsync(true);

            grupo.AdicionarMembro(membro);

            //Act
            await _grupoCommandHandler.Handle(removerMembroGrupoCommand, ApplicationFixtures.CancellationToken);

            //Assert
            grupo.Membros.Should().BeEmpty();
            _applicationFixtures.Mocker.Verify<IGrupoRepository>(r => r.ExcluirMembro(membro), Times.Once);
            _applicationFixtures.Mocker.Verify<IGrupoRepository>(r => r.AtualizarGrupo(grupo), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Nao remove membro do grupo e dispara evento de falha")]
        [Trait("Application", "Testes Grupo Command Handler")]
        public async Task GrupoCommandHandler_Handle_RemoverMembroGrupoCommand_NaoDeveRemoverMembroDoGrupo()
        {
            //Arrange             
            RemoverMembroGrupoCommand removerMembroGrupoCommand = _applicationFixtures.GrupoCommandHandler.CriarRemoverMembroGrupoCommandValido();

            //Act
            await _grupoCommandHandler.Handle(removerMembroGrupoCommand, ApplicationFixtures.CancellationToken);

            //Arrange
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }
    }
}

