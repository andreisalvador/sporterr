using Bogus;
using Bogus.Extensions.Brazil;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using System;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Fixtures
{

    [CollectionDefinition(nameof(EmpresaFixtureCollection))]
    public class EmpresaFixtureCollection : ICollectionFixture<EmpresaFixture> { }

    public class EmpresaFixture : IDisposable
    {
        public Empresa CriarEmpresaValida()
        {
            return new Faker<Empresa>("pt_BR")
                .CustomInstantiator(e => new Empresa(e.Company.CompanyName(), e.Company.Cnpj(), TimeSpan.FromHours(8), TimeSpan.FromHours(18), DiasSemanaFuncionamento.DiasUteis));
        }

        public Empresa CriarEmpresaInvalida()
        {
            return new Empresa("A", "20.188.785/0001-85", TimeSpan.MinValue, TimeSpan.MinValue, (DiasSemanaFuncionamento)1546);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
