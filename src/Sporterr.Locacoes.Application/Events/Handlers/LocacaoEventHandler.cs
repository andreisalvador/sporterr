using MediatR;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Locacoes.Data.Repository.Interfaces;
using Sporterr.Locacoes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Application.Events.Handlers
{
    public class LocacaoEventHandler :
        INotificationHandler<SolicitacaoLocacaoAprovadaEvent>,
        INotificationHandler<SolicitacaoLocacaoRecusadaEvent>
    {
        private readonly ILocacaoRepository _repository;
        private readonly IMediatrHandler _mediatr;
        public LocacaoEventHandler(ILocacaoRepository repository, IMediatrHandler mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
        }

        public async Task Handle(SolicitacaoLocacaoAprovadaEvent message, CancellationToken cancellationToken)
        {
            Locacao locacaoParaAprovar = await _repository.ObterPorId(message.LocacaoId);

            locacaoParaAprovar.AprovarLocacao();

            _repository.AtualizarLocacao(locacaoParaAprovar);

            if (await _repository.Commit())
                await _mediatr.Publish(new LocacaoStatusAtualizadoEvent(locacaoParaAprovar.Id, locacaoParaAprovar.EmpresaId, locacaoParaAprovar.Quadra.Id, locacaoParaAprovar.Status));
        }

        public async Task Handle(SolicitacaoLocacaoRecusadaEvent message, CancellationToken cancellationToken)
        {
            Locacao locacaoParaRecusar = await _repository.ObterPorId(message.LocacaoId);

            locacaoParaRecusar.RecusarLocacao();

            _repository.AtualizarLocacao(locacaoParaRecusar);

            if (await _repository.Commit())
                await _mediatr.Publish(new LocacaoStatusAtualizadoEvent(locacaoParaRecusar.Id, locacaoParaRecusar.EmpresaId, locacaoParaRecusar.Quadra.Id, locacaoParaRecusar.Status));
        }
    }
}
