﻿using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(FixtureWrapper))]
    public class FixtureWrapperCollection : ICollectionFixture<FixtureWrapper> { }

    public class FixtureWrapper : IDisposable
    {
        private readonly Lazy<EmpresaFixture> empresaFixture;
        private readonly Lazy<UsuarioFixture> usuarioFixture;
        private readonly Lazy<GrupoFixture> grupoFixture;
        private readonly Lazy<SolicitacaoFixture> solicitacaoFixture;
        private readonly Lazy<QuadraFixture> quadraFixture;

        public EmpresaFixture Empresa => empresaFixture.Value;
        public UsuarioFixture Usuario => usuarioFixture.Value;
        public GrupoFixture Grupo => grupoFixture.Value;
        public SolicitacaoFixture Solicitacao => solicitacaoFixture.Value;
        public QuadraFixture Quadra => quadraFixture.Value;

        public FixtureWrapper()
        {
            empresaFixture = new Lazy<EmpresaFixture>();
            usuarioFixture = new Lazy<UsuarioFixture>();
            grupoFixture = new Lazy<GrupoFixture>();
            solicitacaoFixture = new Lazy<SolicitacaoFixture>();
            quadraFixture = new Lazy<QuadraFixture>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
