using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.TestFixtures.Application.Fixtures
{
    public class UsuarioCommandHandlerFixture
    {
        private readonly Faker _faker;

        public UsuarioCommandHandlerFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public AdicionarUsuarioCommand CriarAdicionarUsuarioCommandValido()
            => new AdicionarUsuarioCommand(_faker.Name.FirstName(), _faker.Internet.Email(), _faker.Internet.Password());

        public AdicionarUsuarioCommand CriarAdicionarUsuarioCommandInvalido()
            => new AdicionarUsuarioCommand(string.Empty, string.Empty, string.Empty);

        public AdicionarEmpresaCommand CriarAdicionarEmpresaCommandValido()
            => new AdicionarEmpresaCommand(Guid.NewGuid(), _faker.Company.CompanyName(), _faker.Company.Cnpj(), _faker.PickRandom<DiasSemanaFuncionamento>(), TimeSpan.FromHours(8), TimeSpan.FromHours(22));

        public AdicionarEmpresaCommand CriarAdicionarEmpresaCommandInvalido()
            => new AdicionarEmpresaCommand(Guid.Empty, string.Empty, string.Empty, (DiasSemanaFuncionamento)150, TimeSpan.MinValue, TimeSpan.MinValue);

        public AdicionarGrupoCommand CriarAdicionarGrupoCommandValido()
            => new AdicionarGrupoCommand(Guid.NewGuid(), _faker.Internet.DomainWord(), _faker.Random.Byte(0, 100));

        public AdicionarGrupoCommand CriarAdicionarGrupoCommandInvalido()
            => new AdicionarGrupoCommand(Guid.Empty, string.Empty, 0);

        public InativarEmpresaCommand CriarInvativarEmpresaCommandValido()
            => new InativarEmpresaCommand(Guid.NewGuid(), Guid.NewGuid());

        public InativarEmpresaCommand CriarInativarEmpresaCommandInvalido()
            => new InativarEmpresaCommand(Guid.Empty, Guid.Empty);
    }
}
