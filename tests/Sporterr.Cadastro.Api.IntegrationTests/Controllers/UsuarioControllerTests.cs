using FluentAssertions;
using Newtonsoft.Json;
using Sporterr.Cadastro.Api.IntegrationTests.Fixtures;
using Sporterr.Cadastro.Api.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sporterr.Cadastro.Api.IntegrationTests.Controllers
{    
    public class UsuarioControllerTests : IClassFixture<TestFixtures.Api.ApiFixtures<Startup>>
    {
        private readonly TestFixtures.Api.ApiFixtures<Startup> _apiFixtures;

        public UsuarioControllerTests(TestFixtures.Api.ApiFixtures<Startup> apiFixtures)
        {
            _apiFixtures = apiFixtures;
        }

        [Fact(DisplayName = "Adiciona novo usuário")]
        [Trait("Api", "Testes do Cadastro Api")]
        public async Task PerfilHabilidadesController_Novo_DeveCriarNovoPerfilParaOUsuarioERetornarStatus200()
        {
            var httpMethod = new HttpMethod("POST");

            HttpRequestMessage request = new HttpRequestMessage(httpMethod, "/api/Usuario/Novo")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new UsuarioModel
                {
                    Nome = "Andrei",
                    Email = "andreifs@uol.coom",
                    Senha = "senhaaquixx"
                }), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _apiFixtures.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            response.Content.ReadAsStringAsync().Result.Should().Be("Novo usuário criado com sucesso");
        }
    }
}
