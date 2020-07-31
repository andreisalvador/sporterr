using Moq.AutoMock;
using Sporterr.Sorteio.Domain.Services;
using Sporterr.Sorteio.Domain.Services.Interfaces;
using System;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(FixtureWrapper))]
    public class FixtureWrapperCollection : ICollectionFixture<FixtureWrapper> { }
    public class FixtureWrapper : IDisposable
    {
        private readonly Lazy<PerfilHabilidadesFixture> _perfilHabilidadeFixture;
        private readonly Lazy<HabilidadeUsuarioFixture> _habilidadeUsuarioFixture;
        private readonly Lazy<AvaliacaoHabilidadeFixture> _avaliacaoHabilidadeFixture;
        private readonly Lazy<HabilidadeFixture> _habilidadeFixture;
        private readonly Lazy<EsporteFixture> _esporteFixture;

        public PerfilHabilidadesFixture PerfilHabilidades => _perfilHabilidadeFixture.Value;
        public HabilidadeUsuarioFixture HabilidadeUsuario => _habilidadeUsuarioFixture.Value;
        public AvaliacaoHabilidadeFixture AvaliacaoHabilidade => _avaliacaoHabilidadeFixture.Value;
        public HabilidadeFixture Habilidade => _habilidadeFixture.Value;
        public EsporteFixture Esporte => _esporteFixture.Value;
        public AutoMocker Mocker { get; private set; }

        public FixtureWrapper()
        {
            Mocker = new AutoMocker();
            _perfilHabilidadeFixture = new Lazy<PerfilHabilidadesFixture>();
            _habilidadeFixture = new Lazy<HabilidadeFixture>();
            _habilidadeUsuarioFixture = new Lazy<HabilidadeUsuarioFixture>();
            _avaliacaoHabilidadeFixture = new Lazy<AvaliacaoHabilidadeFixture>();
            _esporteFixture = new Lazy<EsporteFixture>();
        }


        public IPerfilServices ObterPerfilService()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<PerfilServices>();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
