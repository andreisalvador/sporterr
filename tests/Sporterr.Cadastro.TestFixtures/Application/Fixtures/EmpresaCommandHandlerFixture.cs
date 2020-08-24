using Bogus;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Core.Enums;
using Sporterr.Tests.Common.Fixtures;
using System;

namespace Sporterr.Cadastro.TestFixtures.Application.Fixtures
{
    public class EmpresaCommandHandlerFixture : BaseFixture
    {
        public AdicionarQuadraCommand CriarAdicionarQuadraCommandValido()
            => new AdicionarQuadraCommand(Guid.NewGuid(), Guid.NewGuid(), Faker.Random.Decimal(0m, 1000m), TimeSpan.FromHours(1), Faker.Random.Enum<TipoEsporte>());

        public AdicionarQuadraCommand CriarAdicionarQuadraCommandInvalido()
            => new AdicionarQuadraCommand(Guid.Empty, Guid.Empty, -1m, TimeSpan.FromHours(15), (TipoEsporte)1564);

        public AprovarSolicitacaoLocacaoCommand CriarAprovarSolicitacaoLocacaoCommandValidoComQuadra(Guid quadraId)
           => new AprovarSolicitacaoLocacaoCommand(Guid.NewGuid(), Guid.NewGuid(), quadraId);
        public AprovarSolicitacaoLocacaoCommand CriarAprovarSolicitacaoLocacaoCommandValido()
            => new AprovarSolicitacaoLocacaoCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        public AprovarSolicitacaoLocacaoCommand CriarAprovarSolicitacaoLocacaoCommandInvalido()
            => new AprovarSolicitacaoLocacaoCommand(Guid.Empty, Guid.Empty, Guid.Empty);

        public RecusarSolicitacaoLocacaoCommand CriarRecusarSolicitacaoLocacaoCommandValido()
            => new RecusarSolicitacaoLocacaoCommand(Guid.NewGuid(), Faker.Lorem.Sentence());

        public RecusarSolicitacaoLocacaoCommand CriarRecusarSolicitacaoLocacaoCommandInvalido()
            => new RecusarSolicitacaoLocacaoCommand(Guid.Empty, string.Empty);

        public CancelarSolicitacaoLocacaoEmpresaCommand CriarCancelarSolicitacaoLocacaoEmpresaCommandValido()
            => new CancelarSolicitacaoLocacaoEmpresaCommand(Guid.NewGuid(), Guid.NewGuid(), Faker.Lorem.Sentence());

        public CancelarSolicitacaoLocacaoEmpresaCommand CriarCancelarSolicitacaoLocacaoEmpresaCommandInvalido()
            => new CancelarSolicitacaoLocacaoEmpresaCommand(Guid.Empty, Guid.Empty, string.Empty);

        public InativarQuadraCommand CriarInativarQuadraCommandValido()
           => new InativarQuadraCommand(Guid.NewGuid(), Guid.NewGuid());

        public InativarQuadraCommand CriarInativarQuadraCommandInvalido()
            => new InativarQuadraCommand(Guid.Empty, Guid.Empty);
    }
}
