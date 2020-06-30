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
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.EventSourcing;
using Sporterr.EventSourcing.Repository;
using Sporterr.Locacoes.Application.Commands;
using Sporterr.Locacoes.Application.Commands.Handlers;
using Sporterr.Locacoes.Application.Events.Handlers;
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
                //Usuario
                .AddScoped<IRequestHandler<AdicionarUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarEmpresaUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<AdicionarGrupoUsuarioCommand, bool>, UsuarioCommandHandler>()
                .AddScoped<IRequestHandler<InativarEmpresaUsuarioCommand, bool>, UsuarioCommandHandler>()

                //Grupo
                .AddScoped<IRequestHandler<AdicionarMembroGrupoCommand, bool>, GrupoCommandHandler>()
                .AddScoped<IRequestHandler<RemoverMembroGrupoCommand, bool>, GrupoCommandHandler>()

                //Empresa
                .AddScoped<IRequestHandler<AdicionarQuadraEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoParaEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<AprovarSolicitacaoEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<RecusarSolicitacaoLocacaoCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<IRequestHandler<InativarQuadraEmpresaCommand, bool>, EmpresaCommandHandler>()
                .AddScoped<INotificationHandler<LocacaoSolicitadaEvent>, EmpresaEventHandler>()
                .AddScoped<INotificationHandler<CancelamentoLocacaoSolicitadoEvent>, EmpresaEventHandler>()

                //Locacao
                .AddScoped<IRequestHandler<AbrirSolicitacaoLocacaoCommand, bool>, LocacaoCommandHandler>()
                .AddScoped<IRequestHandler<SolicitarCancelamentoLocacaoCommand, bool>, LocacaoCommandHandler>()
                .AddScoped<INotificationHandler<SolicitacaoAbertaEvent>, LocacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoCanceladaEvent>, LocacaoEventHandler>()
                .AddScoped<INotificationHandler<CancelamentoLocacaoSolicitadoEvent>, LocacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoRecusadaEvent>, LocacaoEventHandler>()
                .AddScoped<INotificationHandler<SolicitacaoLocacaoAprovadaEvent>, LocacaoEventHandler>()


                //Infra
                .AddScoped<IEmpresaRepository, EmpresaRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<ILocacaoRepository, LocacaoRepository>()
                .AddScoped<IGrupoRepository, GrupoRepository>()
                .AddScoped<IMediatrHandler, MediatrHandler>()
                .AddScoped<EventStoreContext>()
                .AddScoped<IEventSourcingRepository, EventSourcingRepository>()
                .AddDbContext<LocacoesContext>()
                .AddDbContext<CadastroContext>()
                .AddMediatR(typeof(Program))
                .BuildServiceProvider();


            var mediatr = provider.GetService<IMediatrHandler>();


            Task.WaitAll(
                mediatr.Send(new AdicionarUsuarioCommand("Pedro", "pedrofs951@gmail.com", "12345678"))
                // mediatr.Send(new AdicionarGrupoUsuarioCommand(Guid.Parse("e8325aac-b4ab-4534-ae4d-7dcd04b09065"), "Grupo dos caras", 15))
                //mediatr.Send(new AdicionarEmpresaUsuarioCommand(Guid.Parse("e8325aac-b4ab-4534-ae4d-7dcd04b09065"), "PEDRAO LTDA","651864984894", Core.Enums.DiasSemanaFuncionamento.DiasUteis,
                //new TimeSpan(8,0,0), new TimeSpan(18,0,0)))
                //mediatr.Send(new AdicionarMembroGrupoCommand(Guid.Parse("e8325aac-b4ab-4534-ae4d-7dcd04b09065"), Guid.Parse("ffcdde4e-13d5-41a6-b1dd-b10aeccb26dd")))
                //mediatr.Send(new RemoverMembroGrupoCommand(Guid.Parse("790a9bd5-c637-4e42-9c36-863adec9aa9a"), Guid.Parse("ffcdde4e-13d5-41a6-b1dd-b10aeccb26dd")))
                // mediatr.Send(new AdicionarQuadraEmpresaCommand(usuarioGuid, empresaId, 100m, new TimeSpan(1, 0, 0), Core.Enums.Esporte.Futebol))
                //mediatr.Send(new AbrirSolicitacaoLocacaoCommand(Guid.Parse("d26b7a20-b8fd-42db-8dec-97f83ad93315"),
                //                                                Guid.Parse("67118e04-cda6-4aec-a7ec-d3fa7c92824f"),
                //                                                Guid.Parse("a26d52a2-4f61-41c5-adc2-36d876fdd968"), 100m, new TimeSpan(1, 0, 0), DateTime.Now, DateTime.Now))
                //    mediatr.Send(new AprovarSolicitacaoEmpresaCommand(Guid.Parse("958756ca-820e-4289-a63d-9838781d82fd"), Guid.Parse("67118e04-cda6-4aec-a7ec-d3fa7c92824f")))
                //mediatr.Send(new SolicitarCancelamentoLocacaoCommand(Guid.Parse("2bfbd0b3-be29-482e-b8da-d3813957e214"), Guid.Parse("0e16a024-6a0d-4ab5-9b23-a164e48594a9"), "Deu ruim aqui meu."))
                );

        }
    }
}
