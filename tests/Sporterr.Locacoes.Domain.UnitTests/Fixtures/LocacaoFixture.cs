using Sporterr.Locacoes.Domain;
using System;
using Xunit;

namespace Sporterr.Locacoes.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(LocacaoFixtureCollection))]
    public class LocacaoFixtureCollection : ICollectionFixture<LocacaoFixture> { }

    public class LocacaoFixture
    {
        public Locacao CriarLocacaoValida()
        {
            return new Locacao(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), TimeSpan.FromHours(1),
                new Core.DomainObjects.DTO.InformacoesTempoQuadra(150, TimeSpan.FromMinutes(15)));
        }

        public Locacao CriarLocacaoInvalida()
        {
            return new Locacao(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, TimeSpan.MinValue, new Core.DomainObjects.DTO.InformacoesTempoQuadra(0m, TimeSpan.MinValue));
        }
    }
}
