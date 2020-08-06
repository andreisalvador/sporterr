using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Handler;
using Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces;
using Sporterr.EventSourcing;
using Sporterr.EventSourcing.Repository;
using Sporterr.Sorteio.Application.Commands;
using Sporterr.Sorteio.Application.Commands.Handlers;
using Sporterr.Sorteio.Data;
using Sporterr.Sorteio.Data.Repository;
using Sporterr.Sorteio.Data.Seeds;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using Sporterr.Sorteio.Domain.Services;
using Sporterr.Sorteio.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Api.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddContexts(services, configuration);

            AddRepositories(services);
            AddDomainServices(services);
            AddMessaging(services);
            AddBus(services);
            AddEventSourcing(services);
            AddSeeds(services);
        }

        private static void AddContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SorteioContext>(options =>
                            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<SorteioContext>();
        }

        private static void AddSeeds(IServiceCollection services)
        {
            services.AddTransient<IDataSeeder<SorteioContext>, SorteioDataSeeder>();
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
            services.AddScoped<IRequestHandler<AdicionarPerfilHabilidadesCommand, ValidationResult>, PerfilHabilidadesCommandHandler>();
            services.AddScoped<IRequestHandler<VincularEsportePerfilHabilidadesCommand, ValidationResult>, PerfilHabilidadesCommandHandler>();
            services.AddScoped<IRequestHandler<AvaliarHabilidadesUsuarioCommand, ValidationResult>, PerfilHabilidadesCommandHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddScoped<IPerfilServices, PerfilServices>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IPerfilHabilidadesRepository, PerfilHabilidadesRepository>();
            services.AddScoped<IHabilidadeUsuarioRepository, HabilidadeUsuarioRepository>();
            services.AddScoped<IEsporteRepository, EsporteRepository>();
        }
    }
}
