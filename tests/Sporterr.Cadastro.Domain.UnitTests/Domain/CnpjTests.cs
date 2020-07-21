using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Sporterr.Cadastro.Domain.UnitTests.Domain
{
    public class CnpjTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public CnpjTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Teste()
        {
            Cnpj cnpj = "20.188.785/0001-85";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 2_000_000; i++)
                Cnpj.IsValid(cnpj.Value);

            stopwatch.Stop();

            testOutputHelper.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

        }
    }
}
