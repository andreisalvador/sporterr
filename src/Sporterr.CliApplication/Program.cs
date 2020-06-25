using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Cadastro.Application.Commands.Handlers;
using Sporterr.Cadastro.Application.Events.Handlers;
using Sporterr.Cadastro.Data;
using Sporterr.Cadastro.Data.Repository;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Locacoes.Application.Commands;
using Sporterr.Locacoes.Application.Commands.Handlers;
using Sporterr.Locacoes.Data;
using Sporterr.Locacoes.Data.Repository;
using Sporterr.Locacoes.Data.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sporterr.CliApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider provider = new ServiceCollection()
                .AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoCommand, bool>, LocacaoCommandHandler>()
                .AddScoped<IRequestHandler<SolicitarCancelamentoLocacaoCommand, bool>, LocacaoCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarEmpresaUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarGrupoUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<InativarEmpresaUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarQuadraEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoParaEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<AprovarSolicitacaoEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<RecusarSolicitacaoLocacaoCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<InativarQuadraEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<INotificationHandler<LocacaoSolicitadaEvent>, EmpresaEventHandler>()
                .AddScoped<INotificationHandler<CancelamentoLocacaoSolicitadoEvent>, EmpresaEventHandler>()
                .AddScoped<IEmpresaRepository, EmpresaRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<ILocacaoRepository, LocacaoRepository>()
                .AddScoped<IMediatrHandler, MediatrHandler>()
                .AddDbContext<LocacoesContext>()
                .AddDbContext<CadastroContext>()
                .AddMediatR(typeof(Program))
                .BuildServiceProvider();

                        
            var mediatr = provider.GetService<IMediatrHandler>();

            Task.WaitAll(mediatr.Send(new AbrirSolicitacaoLocacaoCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 150, TimeSpan.MinValue, DateTime.Now, DateTime.Now)));

        }
    }
}
