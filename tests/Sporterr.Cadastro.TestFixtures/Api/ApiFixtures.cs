using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Sporterr.Cadastro.TestFixtures.Api
{
    public class ApiFixtures<TStartup> : IDisposable where TStartup : class
    {
        public System.Net.Http.HttpClient Client { get; }

        public ApiFixtures()
        {
            Client = new TestServer(new WebHostBuilder()
                    .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.Testing.json"))
                    .UseEnvironment("Testing")
                    .UseStartup<TStartup>())
                    .CreateClient();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
