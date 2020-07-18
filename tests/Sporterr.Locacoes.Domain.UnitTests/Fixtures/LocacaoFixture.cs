using Sporterr.Locacoes.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Locacoes.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(LocacaoFixtureCollection))]
    public class LocacaoFixtureCollection : ICollectionFixture<LocacaoFixture> { }

    public class LocacaoFixture
    {
        public Locacao CriarLocacaoValida()
        {
            return new Locacao(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), TimeSpan.FromHours(1), new Core.DomainObjects.DTO.InformacoesTempoQuadra
            {
                TempoLocacaoQuadra = TimeSpan.FromMinutes(15),
                ValorPorTempoLocadoQuadra = 150
            });
        }

        public Locacao CriarLocacaoInvalida()
        {
            return new Locacao(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, TimeSpan.MinValue, new Core.DomainObjects.DTO.InformacoesTempoQuadra
            {
                TempoLocacaoQuadra = TimeSpan.MinValue,
                ValorPorTempoLocadoQuadra = 0m
            });
        }
    }
}
