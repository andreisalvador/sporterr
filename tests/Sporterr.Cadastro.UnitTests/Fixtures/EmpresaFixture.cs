using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{

    [CollectionDefinition(nameof(EmpresaFixtureCollection))]
    public class EmpresaFixtureCollection : ICollectionFixture<EmpresaFixture> { }

    public class EmpresaFixture : IDisposable
    {
        public Empresa CriarEmpresaValida()
        {
            return new Empresa("Andrei Solutions", "15405104000153", TimeSpan.FromHours(8), TimeSpan.FromHours(18), Core.Enums.DiasSemanaFuncionamento.DiasUteis);
        }

        public Empresa CriarEmpresaInvalida()
        {
            return new Empresa("A", "", TimeSpan.MinValue, TimeSpan.MinValue, (DiasSemanaFuncionamento)1546);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
