using FluentValidation.Results;
using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.Handler;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Application.Commands.Handlers
{
    public class GrupoCommandHandler : CommandHandler<Grupo>,
        IRequestHandler<AdicionarMembroGrupoCommand, ValidationResult>,
        IRequestHandler<RemoverMembroGrupoCommand, ValidationResult>
    {
        private readonly IGrupoRepository _repository;
        public GrupoCommandHandler(IGrupoRepository repository, IMediatrHandler mediatr) : base(repository, mediatr)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(AdicionarMembroGrupoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult; 

            Grupo grupo = await _repository.ObterGrupoPorId(message.GrupoId);

            if (grupo == null) return await NotifyAndReturn("Grupo não encontrado.");

            Membro novoMembro = new Membro(message.UsuarioMembroId);

            grupo.AdicionarMembro(novoMembro);

            _repository.AdicionarMembro(novoMembro);

            _repository.AtualizarGrupo(grupo);

            return await SaveAndPublish(new MembroAdicionadoGrupoEvent(novoMembro.Id, novoMembro.UsuarioId, novoMembro.GrupoId));
        }

        public async Task<ValidationResult> Handle(RemoverMembroGrupoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            Grupo grupo = await _repository.ObterGrupoPorId(message.GrupoId);

            if (grupo == null) return await NotifyAndReturn("Grupo não encontrado.");

            Membro membro = await _repository.ObterMembroPorId(message.MembroId);

            if (membro == null) return await NotifyAndReturn("Membro não encontrado.");

            grupo.RemoverMembro(membro);

            _repository.ExcluirMembro(membro);

            _repository.AtualizarGrupo(grupo);

            return await SaveAndPublish(new MembroRemovidoGrupoEvent(membro.Id, membro.UsuarioId, membro.GrupoId));
        }
    }
}
