using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Api.IntegrationTests.Fixtures
{
    [CollectionDefinition(nameof(FixtureWrapper))]
    public class FixtureWrapperCollection : ICollectionFixture<FixtureWrapper> { }

    public class FixtureWrapper : IDisposable
    {
        public HttpClient Client { get; }

        public FixtureWrapper()
        {
            Client = new TestServer(new WebHostBuilder()
                    .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.Testing.json"))
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>())
                    .CreateClient();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
