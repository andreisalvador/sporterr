using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Cadastro.Application.Commands.Handlers;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Application.Events.Handlers;
using Sporterr.Cadastro.Data;
using Sporterr.Cadastro.Data.Repository;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Data.Reading;
using Sporterr.Core.Enums;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.EventSourcing;
using Sporterr.EventSourcing.Repository;
using Sporterr.Locacoes.Application.Commands;
using Sporterr.Locacoes.Application.Commands.Handlers;
using Sporterr.Locacoes.Application.Events.Handlers;
using Sporterr.Locacoes.Data;
using Sporterr.Locacoes.Data.Repository;
using Sporterr.Locacoes.Data.Repository.Interfaces;
using Sporterr.Reading;
using Sporterr.Reading.Repository;
using System;
using System.Threading.Tasks;

namespace Sporterr.CliApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider provider = new ServiceCollection()
                //Usuario
                .AddScoped<IRequestHandler<AdicionarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarEmpresaUsuarioCommand, ValidationResult>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarGrupoUsuarioCommand, ValidationResult>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<InativarEmpresaUsuarioCommand, ValidationResult>, UsuarioCommandHandler>()

                //Grupo
                .AddScoped<IRequestHandler<AdicionarMembroGrupoCommand, ValidationResult>, GrupoCommandHandler>()
                .AddScoped<IRequestHandler<RemoverMembroGrupoCommand, ValidationResult>, GrupoCommandHandler>()

                //Empresa
                .AddScoped<IRequestHandler<AdicionarQuadraEmpresaCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<AprovarSolicitacaoLocacaoCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<RecusarSolicitacaoLocacaoCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<InativarQuadraEmpresaCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<INotificationHandler<SolicitacaoCancelamentoLocacaoEnviadaEvent>, EmpresaEventHandler>()

                //Solicitacao
                .AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoCommand, ValidationResult>, SolicitacaoCommandHandler>()
                .AddScoped<IRequestHandler<SolicitarCancelamentoLocacaoCommand, ValidationResult>, SolicitacaoCommandHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoAprovadaEvent>, SolicitacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoRecusadaEvent>, SolicitacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoCanceladaEvent>, SolicitacaoEventHandler>()

                //Infra
                .AddScoped<IEmpresaRepository, EmpresaRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<ILocacaoRepository, LocacaoRepository>()
                .AddScoped<ISolicitacaoRepository, SolicitacaoRepository>()
                .AddScoped<IGrupoRepository, GrupoRepository>()
                .AddScoped<IMediatrHandler, MediatrHandler>()
                .AddScoped<EventStoreContext>()
                .AddScoped<IEventSourcingRepository, EventSourcingRepository>()
                .AddDbContext<LocacoesContext>()
                .AddDbContext<CadastroContext>()
                .AddScoped<IReadOnlyRepository, ReadOnlyRepository>()
                .AddSingleton<RavenDocumentStore>()
                .AddMediatR(typeof(Program))
                .BuildServiceProvider();



            var mediatr = provider.GetService<IMediatrHandler>();


            Task.WaitAll(
                //mediatr.Send(new AdicionarUsuarioCommand("Andrei Franz Salvador", "andreifs95@gmail.com", "12345678"))
                //mediatr.Send(new AdicionarEmpresaUsuarioCommand(Guid.Parse("e1dbb799-d9cb-450e-a8b3-214cd0ffb130"), "Andrei LTDA", "516464844684684", DiasSemanaFuncionamento.DiasUteis, TimeSpan.FromHours(8), TimeSpan.FromHours(18)))
                //mediatr.Send(new AdicionarQuadraEmpresaCommand(Guid.Parse("e1dbb799-d9cb-450e-a8b3-214cd0ffb130"), Guid.Parse("7f4ba969-7fc3-4795-9394-0751e7a09af4"), 150m, TimeSpan.FromHours(1), Esporte.Futebol))
                //mediatr.Send(new AbrirSolicitacaoLocacaoCommand(Guid.Parse("e1dbb799-d9cb-450e-a8b3-214cd0ffb130"), Guid.Parse("7f4ba969-7fc3-4795-9394-0751e7a09af4"),
                //Guid.Parse("01d75211-46ca-4cd1-9d7e-ab3e00e5758d"), DateTime.Today.AddHours(8), DateTime.Today.AddHours(18)))
                //mediatr.Send(new AprovarSolicitacaoLocacaoCommand(Guid.Parse("79461b5a-ca58-427b-a18c-29ee41dbe024"), Guid.Parse("7f4ba969-7fc3-4795-9394-0751e7a09af4"), Guid.Parse("01d75211-46ca-4cd1-9d7e-ab3e00e5758d")))
                mediatr.Send(new SolicitarCancelamentoLocacaoCommand(Guid.Parse("79461b5a-ca58-427b-a18c-29ee41dbe024"), Guid.Parse("5b4f8c44-0b0c-4e72-88f3-d34bb1d5411c")))
                );

        }
    }
}
