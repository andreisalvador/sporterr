using FluentAssertions;
using Sporterr.Sorteio.Api.IntegrationTests.Fixtures;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sporterr.Sorteio.Api.IntegrationTests.Controllers
{
    [Collection(nameof(FixtureWrapper))]
    public class PerfilHabilidadesControllerTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public PerfilHabilidadesControllerTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Adiciona novo perfil de habilidades")]
        [Trait("Api", "Testes do Sorteio Api")]
        public async Task PerfilHabilidadesController_Novo_DeveCriarNovoPerfilParaOUsuarioERetornarStatus200()
        {
            var httpMethod = new HttpMethod("POST");

            HttpResponseMessage response = await _fixtureWrapper.Client
                .SendAsync(new HttpRequestMessage(httpMethod, $"/api/PerfilHabilidades/Novo/{Guid.NewGuid()}"));

            response.EnsureSuccessStatusCode();
            response.Content.ReadAsStringAsync().Result.Should().Be("Novo perfil de habilidades de usuário criado com sucessos");
        }
    }
}
