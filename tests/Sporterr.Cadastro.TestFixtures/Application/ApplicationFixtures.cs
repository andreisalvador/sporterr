using Moq.AutoMock;
using Sporterr.Cadastro.TestFixtures.Application.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sporterr.Cadastro.TestFixtures.Application
{
    public class ApplicationFixtures : IDisposable
    {
        public static readonly CancellationToken CancellationToken = new CancellationToken();
        private readonly Lazy<EmpresaCommandHandlerFixture> _empresaCommandHandlerFixture;
        private readonly Lazy<GrupoCommandHandlerFixture> _grupoCommandHandlerFixture;

        public GrupoCommandHandlerFixture GrupoCommandHandler => _grupoCommandHandlerFixture.Value;
        public EmpresaCommandHandlerFixture EmpresaCommandHandler => _empresaCommandHandlerFixture.Value;
        public AutoMocker Mocker { get; private set; }

        public ApplicationFixtures()
        {
            Mocker = new AutoMocker();
            _empresaCommandHandlerFixture = new Lazy<EmpresaCommandHandlerFixture>();
            _grupoCommandHandlerFixture = new Lazy<GrupoCommandHandlerFixture>();
        }


        public TCommandHandler ObterCommandHandler<TCommandHandler>() where TCommandHandler : class
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<TCommandHandler>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
