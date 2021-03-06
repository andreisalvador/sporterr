﻿using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Cadastro.Application.Commands.Handlers;
using Sporterr.Cadastro.Application.Events.Handlers;
using Sporterr.Cadastro.Data;
using Sporterr.Cadastro.Data.Repository;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Data.Reading;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Handler;
using Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces;
using Sporterr.EventSourcing;
using Sporterr.EventSourcing.Repository;
using Sporterr.Locacoes.Application.Commands;
using Sporterr.Locacoes.Application.Commands.Handlers;
using Sporterr.Locacoes.Application.Events.Handlers;
using Sporterr.Locacoes.Data;
using Sporterr.Locacoes.Data.Repository;
using Sporterr.Locacoes.Domain.Data.Interfaces;
using Sporterr.Reading;
using Sporterr.Reading.Repository;
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

                //Solicitacao
                .AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoCommand, ValidationResult>, SolicitacaoCommandHandler>()
                .AddScoped<IRequestHandler<SolicitarCancelamentoLocacaoCommand, ValidationResult>, SolicitacaoCommandHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoAprovadaEvent>, SolicitacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoRecusadaEvent>, SolicitacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoCanceladaEvent>, SolicitacaoEventHandler>()

                //Perfil
                .AddScoped<IRequestHandler<AdicionarPerfilHabilidadesCommand, ValidationResult>, PerfilHabilidadesCommandHandler>()
                .AddScoped<IRequestHandler<VincularEsportePerfilHabilidadesCommand, ValidationResult>, PerfilHabilidadesCommandHandler>()
                .AddScoped<IRequestHandler<AvaliarHabilidadesUsuarioCommand, ValidationResult>, PerfilHabilidadesCommandHandler>()
                .AddScoped<IPerfilServices, PerfilServices>()

                //Notification
                .AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>()

                //Infra
                .AddScoped<IEmpresaRepository, EmpresaRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<ILocacaoRepository, LocacaoRepository>()
                .AddScoped<ISolicitacaoRepository, SolicitacaoRepository>()
                .AddScoped<IGrupoRepository, GrupoRepository>()
                .AddScoped<IEsporteRepository, EsporteRepository>()
                .AddScoped<IHabilidadeUsuarioRepository, HabilidadeUsuarioRepository>()
                .AddScoped<IPerfilHabilidadesRepository, PerfilHabilidadesRepository>()
                .AddScoped<IEventSourcingRepository, EventSourcingRepository>()
                .AddScoped<IReadOnlyRepository, ReadOnlyRepository>()
                .AddSingleton<RavenDocumentStore>()
                .AddScoped<EventStoreContext>()
                .AddDbContext<LocacoesContext>()
                .AddDbContext<CadastroContext>()
                .AddDbContext<SorteioContext>()
                .AddTransient<IDataSeeder<SorteioContext>, SorteioDataSeeder>()
                .AddScoped<IMediatrHandler, MediatrHandler>()
                //.AddMediatR(typeof(Program))
                .BuildServiceProvider();



            var seeder = provider.GetService<IDataSeeder<SorteioContext>>();
            var mediatr = provider.GetService<IMediatrHandler>();

            // Task.Run(() => seeder.Seed()).Wait();

            // Task.Run(() => mediatr.Send(new AdicionarPerfilHabilidadesCommand(Guid.NewGuid()))).Wait();

            // Task.Run(() => mediatr.Send(new VincularEsportePerfilHabilidadesCommand(Guid.Parse("64269af2-4e7a-43c0-9cbd-ac5aef7de41f"), Guid.Parse("690c869a-d79b-4df6-9c00-ea0334036913")))).Wait();

            var avaliacoes = new Dictionary<Guid, double>()
            {
                {Guid.Parse("8d02e12e-356a-49bf-b134-146483e2227a"), 8},
                {Guid.Parse("94dc5530-b8b2-424e-a7f3-2c7170230e8c"), 10},
                {Guid.Parse("55b37550-ae05-4bec-94c5-f9f65f94564d"), 5}
            };

            Task.Run(() => mediatr.Send(new AvaliarHabilidadesUsuarioCommand(Guid.Parse("64269af2-4e7a-43c0-9cbd-ac5aef7de41f"), avaliacoes))).Wait();



            //Task.WaitAll(
            //    seeder.Seed()
            //    //mediatr.Send(new AdicionarUsuarioCommand("Andrei Franz Salvador", "andreifs95@gmail.com", "12345678"))
            //    //mediatr.Send(new AdicionarEmpresaUsuarioCommand(Guid.Parse("e1dbb799-d9cb-450e-a8b3-214cd0ffb130"), "Andrei LTDA", "516464844684684", DiasSemanaFuncionamento.DiasUteis, TimeSpan.FromHours(8), TimeSpan.FromHours(18)))
            //    //mediatr.Send(new AdicionarQuadraEmpresaCommand(Guid.Parse("e1dbb799-d9cb-450e-a8b3-214cd0ffb130"), Guid.Parse("7f4ba969-7fc3-4795-9394-0751e7a09af4"), 150m, TimeSpan.FromHours(1), TipoEsporte.Futebol))
            //    //mediatr.Send(new AbrirSolicitacaoLocacaoCommand(Guid.Parse("e1dbb799-d9cb-450e-a8b3-214cd0ffb130"), Guid.Parse("7f4ba969-7fc3-4795-9394-0751e7a09af4"),
            //    //Guid.Parse("01d75211-46ca-4cd1-9d7e-ab3e00e5758d"), DateTime.Today.AddHours(8), DateTime.Today.AddHours(18)))
            //    //mediatr.Send(new AprovarSolicitacaoLocacaoCommand(Guid.Parse("79461b5a-ca58-427b-a18c-29ee41dbe024"), Guid.Parse("7f4ba969-7fc3-4795-9394-0751e7a09af4"), Guid.Parse("01d75211-46ca-4cd1-9d7e-ab3e00e5758d")))
            //    //mediatr.Send(new SolicitarCancelamentoLocacaoCommand(Guid.Parse("79461b5a-ca58-427b-a18c-29ee41dbe024"), Guid.Parse("5b4f8c44-0b0c-4e72-88f3-d34bb1d5411c")))
            //    );

        }
    }
}
