using FluentAssertions;
using Sporterr.Cadastro.Api.Models;
using Sporterr.Tests.Common.Api;
using Sporterr.Tests.Common.Extensions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sporterr.Cadastro.Api.IntegrationTests.Controllers
{
    public class UsuarioControllerTests : IClassFixture<LocalTestServer<Startup>>
    {
        private readonly LocalTestServer<Startup> _localTesteServer;

        public UsuarioControllerTests(LocalTestServer<Startup> localTestServer)
        {
            _localTesteServer = localTestServer;
        }

        [Fact(DisplayName = "Adiciona novo usuário")]
        [Trait("Api", "Testes do Cadastro Api")]
        public async Task PerfilHabilidadesController_Novo_DeveCriarNovoPerfilParaOUsuarioERetornarStatus200()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/Usuario/Novo");

            var usuario = new UsuarioModel
            {
                Nome = "Andrei",
                Email = "andreifs@uol.coom",
                Senha = "senhaaquixx"
            };

            request.Content = usuario.ToStringContent();

            HttpResponseMessage response = await _localTesteServer.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            response.Content.ReadAsStringAsync().Result.Should().Be("Novo usuário criado com sucesso");
        }
    }
}
