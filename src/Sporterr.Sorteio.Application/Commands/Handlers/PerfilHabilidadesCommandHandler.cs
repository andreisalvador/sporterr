using FluentValidation.Results;
using MediatR;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.Handler;
using Sporterr.Sorteio.Application.Events;
using Sporterr.Sorteio.Domain;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using Sporterr.Sorteio.Domain.Services.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Application.Commands.Handlers
{
    public class PerfilHabilidadesCommandHandler : CommandHandler<PerfilHabilidades>,
        IRequestHandler<AdicionarPerfilHabilidadesCommand, ValidationResult>,
        IRequestHandler<VincularEsportePerfilHabilidadesCommand, ValidationResult>,
        IRequestHandler<AvaliarHabilidadesUsuarioCommand, ValidationResult>
    {
        private readonly IPerfilHabilidadesRepository _perfilHabilidadesRepository;
        private readonly IPerfilServices _perfilServices;
        public PerfilHabilidadesCommandHandler(IPerfilServices perfilServices, IPerfilHabilidadesRepository repository, IMediatrHandler mediatr) : base(repository, mediatr)
        {
            _perfilHabilidadesRepository = repository;
            _perfilServices = perfilServices;
        }

        public async Task<ValidationResult> Handle(AdicionarPerfilHabilidadesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            if (await _perfilHabilidadesRepository.ExisteParaUsuario(message.UsuarioId))
                return await NotifyAndReturn("Perfil já existente.");

            PerfilHabilidades novoPerfil = new PerfilHabilidades(message.UsuarioId);

            _perfilHabilidadesRepository.AdicionarPerfilHabilidades(novoPerfil);

            return await SaveAndPublish(new PerfilHabilidadesCriadoEvent(novoPerfil.Id, novoPerfil.UsuarioId));
        }

        public async Task<ValidationResult> Handle(VincularEsportePerfilHabilidadesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            await _perfilServices.AdicionarNovoEsporte(message.PerfilHabilidadesId, message.EsporteId);

            return await PublishEvents(new EsporteVinculadoPerfilHabilidadesEvent(message.PerfilHabilidadesId, message.EsporteId));
        }

        public async Task<ValidationResult> Handle(AvaliarHabilidadesUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            await _perfilServices.AvaliarPerfil(message.PerfilHabilidadesId, message.HabilidadesAvaliadas);

            return await PublishEvents(message.HabilidadesAvaliadas.Select(avaliacao => new HabilidadeUsuarioAvaliadaEvent(avaliacao.Key, avaliacao.Value)).ToArray());
        }
    }
}
