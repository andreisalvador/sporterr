
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.EventSourcing;
using Sporterr.EventSourcing.Repository;
using Sporterr.Locacoes.Application.Commands;
using Sporterr.Locacoes.Application.Commands.Handlers;
using Sporterr.Locacoes.Application.Events.Handlers;
using Sporterr.Locacoes.Data;
using Sporterr.Locacoes.Data.Repository;
using Sporterr.Locacoes.Domain.Data.Interfaces;

namespace Sporterr.Locacoes.Api.Extensions
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
            services.AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoCommand, ValidationResult>, SolicitacaoCommandHandler>()
                .AddScoped<IRequestHandler<SolicitarCancelamentoLocacaoCommand, ValidationResult>, SolicitacaoCommandHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoAprovadaEvent>, SolicitacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoRecusadaEvent>, SolicitacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoCanceladaEvent>, SolicitacaoEventHandler>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();
        }

        private static void AddContexts(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<LocacoesContext>(options =>
                         options.UseInMemoryDatabase("LocacoesTestingDb"));
            }
            else
            {
                services.AddDbContext<LocacoesContext>(options =>
                         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<LocacoesContext>();
            };
        }
    }
}
