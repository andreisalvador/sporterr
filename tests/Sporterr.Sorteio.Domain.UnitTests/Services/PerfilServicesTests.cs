using Bogus;
using FluentAssertions;
using Moq;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Enums;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using Sporterr.Sorteio.Domain.Esportes;
using Sporterr.Sorteio.Domain.Services.Interfaces;
using Sporterr.Sorteio.Domain.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Services
{
    [Collection(nameof(FixtureWrapper))]
    public class PerfilServicesTests
    {
        private readonly FixtureWrapper _fixtureWrapper;
        private readonly IPerfilServices _perfilService;

        public PerfilServicesTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
            _perfilService = _fixtureWrapper.ObterPerfilService();

        }

        [Fact(DisplayName = "Adiciona novo esporte ao perfil")]
        [Trait("Domain", "Testes dos serviços do perfil")]
        public void PerfilServices_AdicionarNovoEsporte_DeveAdicionarNovoEsporte()
        {
            //Arrange
            IPerfilServices perfilServices = _perfilService;
            Guid perfilId = Guid.NewGuid();
            Guid esporteId = Guid.NewGuid();
            PerfilHabilidades perfil = new PerfilHabilidades(Guid.NewGuid());
            Esporte esporte = new Futebol();
            _fixtureWrapper.Mocker.GetMock<IPerfilHabilidadesRepository>().Setup(r => r.ObterPorId(perfilId))
                .ReturnsAsync(perfil);

            _fixtureWrapper.Mocker.GetMock<IEsporteRepository>().Setup(r => r.ObterEsporteComHabilidadesPorId(esporteId))
                .ReturnsAsync(esporte);

            //Act
            perfilServices.AdicionarNovoEsporte(perfilId, esporteId);

            //Assert
            perfil.HabilidadesUsuario.Should().HaveCount(esporte.Habilidades.Count);

            perfil.EsportesUsuario.Should().HaveFlag(TipoEsporte.Futebol);

            _fixtureWrapper.Mocker.GetMock<IHabilidadeUsuarioRepository>()
               .Verify(x => x.AdicionarHabilidadesUsuario(It.IsAny<IList<HabilidadeUsuario>>()), Times.Once);

            _fixtureWrapper.Mocker.GetMock<IPerfilHabilidadesRepository>()
               .Verify(x => x.AtualizarPerfilHabilidades(perfil), Times.Once);

            _fixtureWrapper.Mocker.GetMock<IHabilidadeUsuarioRepository>()
              .Verify(x => x.Commit(), Times.Once);

            _fixtureWrapper.Mocker.GetMock<IPerfilHabilidadesRepository>()
              .Verify(x => x.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Notifica e não adiciona novo esporte ao perfil")]
        [Trait("Domain", "Testes dos serviços do perfil")]
        public void PerfilServices_AdicionarNovoEsporte_NaoDeveAdicionarNovoEsporteEDeveNotificar()
        {
            //Arrange
            IPerfilServices perfilServices = _perfilService;

            //Act
            perfilServices.AdicionarNovoEsporte(Guid.NewGuid(), Guid.NewGuid());

            //Assert
            _fixtureWrapper.Mocker.GetMock<IMediatrHandler>()
                .Verify(x => x.Notify(It.IsAny<DomainNotification>()), Times.Exactly(2));
        }

        [Fact(DisplayName = "Avalia habilidades do usuario")]
        [Trait("Domain", "Testes dos serviços do perfil")]
        public void PerfilServices_AvaliarPerfil_DeveAdicionarAvaliacaoNasHabilidadesDoUsuario()
        {
            //Arrange
            IPerfilServices perfilServices = _perfilService;
            PerfilHabilidades perfil = _fixtureWrapper.PerfilHabilidades.CriarPerfilHabilidadeValido();

            for (int i = 0; i < 5; i++)
                perfil.AdicionarHabilidadeUsuario(_fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido());

            IDictionary<Guid, double> habilidadesAvaliadas = new Dictionary<Guid, double>();

            Faker faker = new Faker("pt_BR");

            for (int i = 0; i < 5; i++)
                habilidadesAvaliadas.Add(perfil.HabilidadesUsuario.ElementAt(i).Id, faker.Random.Double(0, 10));

            _fixtureWrapper.Mocker.GetMock<IPerfilHabilidadesRepository>()
                .Setup(x => x.ObterPorIdComHabilidades(perfil.Id))
                .ReturnsAsync(perfil);

            //Act
            perfilServices.AvaliarPerfil(perfil.Id, habilidadesAvaliadas);

            //Assert
            perfil.HabilidadesUsuario.SelectMany(h => h.Avaliacoes).Should().HaveCount(habilidadesAvaliadas.Count);

            _fixtureWrapper.Mocker.GetMock<IHabilidadeUsuarioRepository>()
               .Verify(x => x.AdicionarAvaliacaoHabilidade(It.IsAny<AvaliacaoHabilidade>()), Times.Exactly(habilidadesAvaliadas.Count));

            _fixtureWrapper.Mocker.GetMock<IHabilidadeUsuarioRepository>()
                .Verify(x => x.AtualizarHabilidadesUsuario(It.IsAny<IEnumerable<HabilidadeUsuario>>()), Times.Once);

            _fixtureWrapper.Mocker.GetMock<IPerfilHabilidadesRepository>()
              .Verify(x => x.AtualizarPerfilHabilidades(perfil), Times.Once);

            _fixtureWrapper.Mocker.GetMock<IHabilidadeUsuarioRepository>()
            .Verify(x => x.Commit(), Times.Once);

            _fixtureWrapper.Mocker.GetMock<IPerfilHabilidadesRepository>()
              .Verify(x => x.Commit(), Times.Once);

        }

        [Fact(DisplayName = "Notifica e não avalia habilidades do usuario")]
        [Trait("Domain", "Testes dos serviços do perfil")]
        public void PerfilServices_AvaliarPerfil_NaoDeveAdicionarAvaliacaoNasHabilidadesDoUsuarioEDeveNotificar()
        {
            //Arrange
            IPerfilServices perfilServices = _perfilService;

            //Act
            perfilServices.AvaliarPerfil(Guid.NewGuid(), default);

            //Assert
            _fixtureWrapper.Mocker.GetMock<IMediatrHandler>()
                .Verify(x => x.Notify(It.IsAny<DomainNotification>()), Times.Once);
        }
    }
}
