using MediatR;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Application.Events.Handlers
{
    public class EmpresaEventHandler :
        INotificationHandler<SolicitacaoLocacaoEnviadaEvent>
    {
        private readonly IEmpresaRepository _repository;
        private readonly IMediatrHandler _mediatr;

        public EmpresaEventHandler(IEmpresaRepository repository, IMediatrHandler mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
        }

        public async Task Handle(SolicitacaoLocacaoEnviadaEvent message, CancellationToken cancellationToken)
        {
            await _mediatr.Send(new AdicionarSolicitacaoEmpresaCommand(message.LocacaoId, message.EmpresaId, message.QuadraId));
        }
    }
}
