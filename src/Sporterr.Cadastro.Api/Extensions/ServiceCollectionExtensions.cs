using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sporterr.Cadastro.Data;
using Sporterr.Core.Communication.Mediator;
using System;
using Microsoft.Extensions.Configuration;
using Sporterr.EventSourcing.Repository;
using Sporterr.EventSourcing;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Cadastro.Application.Commands;
using MediatR;
using FluentValidation.Results;
using Sporterr.Cadastro.Application.Commands.Handlers;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Cadastro.Application.Events.Handlers;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Handler;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using Sporterr.Cadastro.Data.Repository;

namespace Sporterr.Cadastro.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            AddContexts(services, configuration, hostEnvironment);
            AddRepositories(services);
            //AddDomainServices(services);
            AddMessaging(services);
            AddBus(services);
            AddEventSourcing(services);
        }

        private static void AddEventSourcing(IServiceCollection services)
        {
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<EventStoreContext>();
        }

        private static void AddBus(IServiceCollection services)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();
        }

        private static void AddMessaging(IServiceCollection services)
        {
            services
                //Usuario
                .AddScoped<IRequestHandler<AdicionarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarEmpresaCommand, ValidationResult>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarGrupoCommand, ValidationResult>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<InativarEmpresaCommand, ValidationResult>, UsuarioCommandHandler>()

                //Grupo
                .AddScoped<IRequestHandler<AdicionarMembroCommand, ValidationResult>, GrupoCommandHandler>()
                .AddScoped<IRequestHandler<RemoverMembroGrupoCommand, ValidationResult>, GrupoCommandHandler>()

                //Empresa
                .AddScoped<IRequestHandler<AdicionarQuadraCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<AprovarSolicitacaoLocacaoCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<RecusarSolicitacaoLocacaoCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<InativarQuadraCommand, ValidationResult>, EmpresaCommandHandler>()
                .AddScoped<INotificationHandler<SolicitacaoCancelamentoLocacaoEnviadaEvent>, EmpresaEventHandler>()
                .AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
        }

        private static void AddContexts(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<CadastroContext>(options =>
                         options.UseInMemoryDatabase("CadastroTestingDb"));
            }
            else
            {
                services.AddDbContext<CadastroContext>(options =>
                         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<CadastroContext>();
            }
        }
    }
}
