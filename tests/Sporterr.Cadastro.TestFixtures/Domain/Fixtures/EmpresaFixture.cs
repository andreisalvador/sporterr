using Bogus.Extensions.Brazil;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Enums;
using Sporterr.Tests.Common.Fixtures;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain.Fixtures
{
    public class EmpresaFixture : BaseFixture, IDisposable
    {
        public Empresa CriarEmpresaValida()
        {
            return NewFakerInstance<Empresa>()
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
