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
        INotificationHandler<SolicitacaoAbertaEvent>,
        INotificationHandler<SolicitacaoLocacaoAprovadaEvent>,
        INotificationHandler<SolicitacaoLocacaoRecusadaEvent>,
        INotificationHandler<SolicitacaoLocacaoCanceladaEvent>,
        INotificationHandler<CancelamentoLocacaoSolicitadoEvent>
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

        public async Task Handle(CancelamentoLocacaoSolicitadoEvent message, CancellationToken cancellationToken)
        {
            Locacao locacaoParaAguardarCancelamento = await _repository.ObterPorId(message.LocacaoId);

            locacaoParaAguardarCancelamento.AguardarCancelamento();

            _repository.AtualizarLocacao(locacaoParaAguardarCancelamento);

            if (await _repository.Commit())
                await _mediatr.Publish(new LocacaoStatusAtualizadoEvent(locacaoParaAguardarCancelamento.Id, locacaoParaAguardarCancelamento.EmpresaId, locacaoParaAguardarCancelamento.Quadra.Id, locacaoParaAguardarCancelamento.Status));
        }

        public async Task Handle(SolicitacaoLocacaoCanceladaEvent message, CancellationToken cancellationToken)
        {
            Locacao locacaoParaCancelar = await _repository.ObterPorId(message.LocacaoId);

            locacaoParaCancelar.CancelarLocacao();

            _repository.AtualizarLocacao(locacaoParaCancelar);

            if (await _repository.Commit())
                await _mediatr.Publish(new LocacaoStatusAtualizadoEvent(locacaoParaCancelar.Id, locacaoParaCancelar.EmpresaId, locacaoParaCancelar.Quadra.Id, locacaoParaCancelar.Status));
        }

        public async Task Handle(SolicitacaoAbertaEvent message, CancellationToken cancellationToken)
        {
            Locacao locacaoParaAguardarAprovacao = await _repository.ObterPorId(message.LocaocaId);

            locacaoParaAguardarAprovacao.AguardarAprovacao(message.SolicitacaoId);

            _repository.AtualizarLocacao(locacaoParaAguardarAprovacao);

            if (await _repository.Commit())
                await _mediatr.Publish(new LocacaoStatusAtualizadoEvent(locacaoParaAguardarAprovacao.Id, locacaoParaAguardarAprovacao.EmpresaId, locacaoParaAguardarAprovacao.Quadra.Id, locacaoParaAguardarAprovacao.Status));
        }
    }
}
