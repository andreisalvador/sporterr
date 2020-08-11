using Bogus;
using Sporterr.Cadastro.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.TestFixtures.Application.Fixtures
{
    public class GrupoCommandHandlerFixture
    {
        private readonly Faker _faker;

        public GrupoCommandHandlerFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public AdicionarMembroCommand CriarAdicionarMembroCommandValido()
            => new AdicionarMembroCommand(Guid.NewGuid(), Guid.NewGuid());

        public AdicionarMembroCommand CriarAdicionarMembroCommandInvalido()
            => new AdicionarMembroCommand(Guid.Empty, Guid.Empty);

        public RemoverMembroGrupoCommand CriarRemoverMembroGrupoCommandValido()
            => new RemoverMembroGrupoCommand(Guid.NewGuid(), Guid.NewGuid());

        public RemoverMembroGrupoCommand CriarRemoverMembroGrupoCommandInvalido()
            => new RemoverMembroGrupoCommand(Guid.Empty, Guid.Empty);
    }
}
