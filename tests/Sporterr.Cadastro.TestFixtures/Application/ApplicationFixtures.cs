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
        private readonly Lazy<UsuarioCommandHandlerFixture> _usuarioCommandHandlerFixture;

        public GrupoCommandHandlerFixture GrupoCommandHandler => _grupoCommandHandlerFixture.Value;
        public EmpresaCommandHandlerFixture EmpresaCommandHandler => _empresaCommandHandlerFixture.Value;
        public UsuarioCommandHandlerFixture UsuarioCommandHandler => _usuarioCommandHandlerFixture.Value;
        public AutoMocker Mocker { get; private set; }

        public ApplicationFixtures()
        {
            Mocker = new AutoMocker();
            _empresaCommandHandlerFixture = new Lazy<EmpresaCommandHandlerFixture>();
            _grupoCommandHandlerFixture = new Lazy<GrupoCommandHandlerFixture>();
            _usuarioCommandHandlerFixture = new Lazy<UsuarioCommandHandlerFixture>();
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
