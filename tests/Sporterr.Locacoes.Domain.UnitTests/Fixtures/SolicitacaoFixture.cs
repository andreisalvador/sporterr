﻿using Sporterr.Locacoes.Domain;
using Sporterr.Tests.Common.Fixtures;
using System;
using Xunit;

namespace Sporterr.Locacoes.UnitTests.Fixtures
{

    [CollectionDefinition(nameof(SolicitacaoFixtureCollection))]
    public class SolicitacaoFixtureCollection : ICollectionFixture<SolicitacaoFixture> { }

    public class SolicitacaoFixture : BaseFixture, IDisposable
    {
        public Solicitacao CriarSolicitacaoValida()
        {
            return NewFakerInstance<Solicitacao>()
                .CustomInstantiator(s => new Solicitacao(s.Random.Guid(), s.Random.Guid(), s.Random.Guid(),
                            s.Date.Between(new DateTime(2020, 7, 6, 15, 0, 0), new DateTime(2020, 7, 6, 15, 30, 0)),
                             s.Date.Between(new DateTime(2020, 7, 6, 15, 31, 0), new DateTime(2020, 7, 6, 16, 0, 0))));
        }

        public Solicitacao CriarSolicitacaoInvalida()
        {
            return new Solicitacao(Guid.Empty, Guid.Empty, Guid.Empty, DateTime.MinValue, DateTime.MinValue);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
