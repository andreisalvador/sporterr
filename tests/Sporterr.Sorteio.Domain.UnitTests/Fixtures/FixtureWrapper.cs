using System;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(FixtureWrapper))]
    public class FixtureWrapperCollection : ICollectionFixture<FixtureWrapper> { }
    public class FixtureWrapper : IDisposable
    {
        private Lazy<PerfilHabilidadesFixture> _perfilHabilidadeFixture;
        private Lazy<HabilidadeUsuarioFixture> _habilidadeUsuarioFixture;
        private Lazy<AvaliacaoHabilidadeFixture> _avaliacaoHabilidadeFixture;
        private Lazy<HabilidadeFixture> _habilidadeFixture;
        private Lazy<EsporteFixture> _esporteFixture;

        public PerfilHabilidadesFixture PerfilHabilidades => _perfilHabilidadeFixture.Value;
        public HabilidadeUsuarioFixture HabilidadeUsuario => _habilidadeUsuarioFixture.Value;
        public AvaliacaoHabilidadeFixture AvaliacaoHabilidade => _avaliacaoHabilidadeFixture.Value;
        public HabilidadeFixture Habilidade => _habilidadeFixture.Value;
        public EsporteFixture Esporte => _esporteFixture.Value;

        public FixtureWrapper()
        {
            _perfilHabilidadeFixture = new Lazy<PerfilHabilidadesFixture>();
            _habilidadeFixture = new Lazy<HabilidadeFixture>();
            _habilidadeUsuarioFixture = new Lazy<HabilidadeUsuarioFixture>();
            _avaliacaoHabilidadeFixture = new Lazy<AvaliacaoHabilidadeFixture>();
            _esporteFixture = new Lazy<EsporteFixture>();
        }


        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
