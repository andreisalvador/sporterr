using FluentAssertions;
using Moq;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Cadastro.Application.Commands.Handlers;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using Sporterr.Cadastro.TestFixtures.Application;
using Sporterr.Cadastro.TestFixtures.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sporterr.Cadastro.Application.UnitTests.Handlers
{
    public class EmpresaCommandHandlerTests : IClassFixture<ApplicationFixtures>, IClassFixture<DomainFixtures>
    {
        private readonly ApplicationFixtures _applicationFixtures;
        private readonly DomainFixtures _domainFixtures;
        private readonly EmpresaCommandHandler _empresaCommandHandler;        

        public EmpresaCommandHandlerTests(ApplicationFixtures applicationFixtures, DomainFixtures domainFixtures)
        {
            _applicationFixtures = applicationFixtures;
            _domainFixtures = domainFixtures;
            _empresaCommandHandler = _applicationFixtures.ObterCommandHandler<EmpresaCommandHandler>();
        }

        [Fact(DisplayName = "Adiciona nova quadra na empresa")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_AdicionarQuadraCommand_DeverAdicionarNovaQuadraNaEmpresa()
        {
            //Arrange
            AdicionarQuadraCommand adicionarQuadraCommand = _applicationFixtures.EmpresaCommandHandler.CriarAdicionarQuadraCommandValido();
            Empresa empresa = _domainFixtures.Empresa.CriarEmpresaValida();
            _applicationFixtures.Mocker.GetMock<IEmpresaRepository>().Setup(r => r.ObterEmpresaPorId(adicionarQuadraCommand.EmpresaId)).ReturnsAsync(empresa);
            _applicationFixtures.Mocker.GetMock<IEmpresaRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _empresaCommandHandler.Handle(adicionarQuadraCommand, ApplicationFixtures.CancellationToken);

            //Assert
            empresa.Quadras.Should().HaveCount(1);
            _applicationFixtures.Mocker.Verify<IEmpresaRepository>(r => r.AdicionarQuadra(It.IsAny<Quadra>()), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Não adiciona nova quadra na empresa")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_AdicionarQuadraCommand_NaoDeveAdicionarNovaQuadraNaEmpresaPoisEmpresaNaoFoiEncontrada()
        {
            //Arrange
            AdicionarQuadraCommand adicionarQuadraCommand = _applicationFixtures.EmpresaCommandHandler.CriarAdicionarQuadraCommandValido();

            //Act
            await _empresaCommandHandler.Handle(adicionarQuadraCommand, ApplicationFixtures.CancellationToken);

            //Assert            
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.Once);
        }

        [Fact(DisplayName = "Disparar evento de solicitação de locação aprovada")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_AprovarSolicitacaoLocacaoCommand_DeveDispararEventoDeSolicitacaoAprovada()
        {
            //Arrange

            Quadra quadra = _domainFixtures.Quadra.CriarQuadraValida();
            Empresa empresa = _domainFixtures.Empresa.CriarEmpresaValida();
            empresa.AdicionarQuadra(quadra);

            AprovarSolicitacaoLocacaoCommand aprovarSolicitacaoLocacaoCommand = _applicationFixtures.EmpresaCommandHandler.CriarAprovarSolicitacaoLocacaoCommandValidoComQuadra(quadra.Id);

            _applicationFixtures.Mocker.GetMock<IEmpresaRepository>().Setup(r => r.ObterEmpresaComQuadrasPorId(aprovarSolicitacaoLocacaoCommand.EmpresaId)).ReturnsAsync(empresa);

            //Act
            await _empresaCommandHandler.Handle(aprovarSolicitacaoLocacaoCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Não dispara evento de solicitação de locação aprovada")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_AprovarSolicitacaoLocacaoCommand_NaoDeveDispararEventoDeSolicitacaoAprovada()
        {
            //Arrange
            AprovarSolicitacaoLocacaoCommand aprovarSolicitacaoLocacaoCommand = _applicationFixtures.EmpresaCommandHandler.CriarAprovarSolicitacaoLocacaoCommandValido();

            //Act
            await _empresaCommandHandler.Handle(aprovarSolicitacaoLocacaoCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce());
        }

        [Fact(DisplayName = "Dispara evento de solicitação de locação recusada")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_RecusarSolicitacaoLocacaoCommand_DeveDispararEventoDeSolicitacaoRecusada()
        {
            //Arrange
            RecusarSolicitacaoLocacaoCommand recusarSolicitacaoLocacaoCommand = _applicationFixtures.EmpresaCommandHandler.CriarRecusarSolicitacaoLocacaoCommandValido();

            //Act
            await _empresaCommandHandler.Handle(recusarSolicitacaoLocacaoCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Dispara evento de solicitação de locação cancelada")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_RecusarSolicitacaoLocacaoCommand_DeveDispararEventoDeSolicitacaoCancelada()
        {
            //Arrange
            CancelarSolicitacaoLocacaoEmpresaCommand recusarSolicitacaoLocacaoCommand = _applicationFixtures.EmpresaCommandHandler.CriarCancelarSolicitacaoLocacaoEmpresaCommandValido();

            //Act
            await _empresaCommandHandler.Handle(recusarSolicitacaoLocacaoCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Inativa quadra da empresa e dispara evento")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_InativarQuadraCommand_DeveInativarQuadraDaEmpresa()
        {
            //Arrange
            InativarQuadraCommand inativarEmpresaCommand = _applicationFixtures.EmpresaCommandHandler.CriarInativarQuadraCommandValido();
            Quadra quadra = _domainFixtures.Quadra.CriarQuadraValida();
            Empresa empresa = _domainFixtures.Empresa.CriarEmpresaValida();
            empresa.AdicionarQuadra(quadra);
            _applicationFixtures.Mocker.GetMock<IEmpresaRepository>().Setup(r => r.ObterEmpresaPorId(inativarEmpresaCommand.EmpresaId)).ReturnsAsync(empresa);
            _applicationFixtures.Mocker.GetMock<IEmpresaRepository>().Setup(r => r.ObterQuadraPorId(inativarEmpresaCommand.QuadraId)).ReturnsAsync(quadra);
            _applicationFixtures.Mocker.GetMock<IEmpresaRepository>().Setup(r => r.Commit()).ReturnsAsync(true);

            //Act
            await _empresaCommandHandler.Handle(inativarEmpresaCommand, ApplicationFixtures.CancellationToken);

            //Assert
            quadra.Ativo.Should().BeFalse();
            _applicationFixtures.Mocker.Verify<IEmpresaRepository>(r => r.AtualizarQuadra(quadra), Times.Once);
            _applicationFixtures.Mocker.Verify<IEmpresaRepository>(r => r.AtualizarEmpresa(empresa), Times.Once);
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Publish(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Não inativa quadra da empresa e dispara evento de falha")]
        [Trait("Application", "Empresa Command Handler")]
        public async Task EmpresaCommandHandler_Handle_InativarQuadraCommand_NaoDeveInativarQuadraDaEmpresa()
        {
            //Arrange
            InativarQuadraCommand inativarEmpresaCommand = _applicationFixtures.EmpresaCommandHandler.CriarInativarQuadraCommandValido();

            //Act
            await _empresaCommandHandler.Handle(inativarEmpresaCommand, ApplicationFixtures.CancellationToken);

            //Assert
            _applicationFixtures.Mocker.Verify<IMediatrHandler>(m => m.Notify(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }
    }
}
