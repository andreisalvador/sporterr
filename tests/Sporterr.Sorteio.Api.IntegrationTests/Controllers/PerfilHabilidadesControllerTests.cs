using FluentAssertions;
using Sporterr.Sorteio.Api.IntegrationTests.Fixtures;
using Sporterr.Tests.Common.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sporterr.Sorteio.Api.IntegrationTests.Controllers
{
    public class PerfilHabilidadesControllerTests : IClassFixture<LocalTestServer<Startup>>
    {
        private readonly LocalTestServer<Startup> _localTesteServer;

        public PerfilHabilidadesControllerTests(LocalTestServer<Startup> localTestServer)
        {
            _localTesteServer = localTestServer;
        }

        [Fact(DisplayName = "Adiciona novo perfil de habilidades")]
        [Trait("Api", "Testes do Sorteio Api")]
        public async Task PerfilHabilidadesController_Novo_DeveCriarNovoPerfilParaOUsuarioERetornarStatus200()
        {
            HttpResponseMessage response = await _localTesteServer.Client
                .SendAsync(new HttpRequestMessage(HttpMethod.Post, $"/api/PerfilHabilidades/Novo/{Guid.NewGuid()}"));

            response.EnsureSuccessStatusCode();
            response.Content.ReadAsStringAsync().Result.Should().Be("Novo perfil de habilidades de usuário criado com sucesso");
        }
    }
}
