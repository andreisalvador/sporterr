using FluentAssertions;
using System;
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
            var before2 = GC.CollectionCount(2);
            var before1 = GC.CollectionCount(1);
            var before0 = GC.CollectionCount(0);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 2_000_000; i++)
                Cnpj.IsValid("20188785000185");

            stopwatch.Stop();

            testOutputHelper.WriteLine($"GC Gen #2 : {GC.CollectionCount(2) - before2}");
            testOutputHelper.WriteLine($"GC Gen #1 : {GC.CollectionCount(1) - before1}");
            testOutputHelper.WriteLine($"GC Gen #0 : {GC.CollectionCount(0) - before0}");
            testOutputHelper.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
