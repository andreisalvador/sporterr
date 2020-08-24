using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;

namespace Sporterr.Tests.Common.Api
{
    public class LocalTestServer<TStartup> : IDisposable where TStartup : class
    {
        public System.Net.Http.HttpClient Client { get; }
        protected virtual string TestSettings => "appsettings.Testing.json";
        protected virtual string Environment => "Testing";

        public LocalTestServer()
        {
            Client = new TestServer(new WebHostBuilder()
                    .ConfigureAppConfiguration(config => config.AddJsonFile(TestSettings))
                    .UseEnvironment(Environment)
                    .UseStartup<TStartup>())
                    .CreateClient();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
