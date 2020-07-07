using MediatR;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Locacoes.Data.Repository.Interfaces;
using Sporterr.Locacoes.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Application.Events.Handlers
{
    public class SolicitacaoEventHandler :
        INotificationHandler<SolicitacaoLocacaoAprovadaEvent>,
        INotificationHandler<SolicitacaoLocacaoRecusadaEvent>,
        INotificationHandler<SolicitacaoLocacaoCanceladaEvent>        
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly IMediatrHandler _mediatr;
        public SolicitacaoEventHandler(ISolicitacaoRepository solicitacaoRepository, ILocacaoRepository repository, IMediatrHandler mediatr)
        {
            _locacaoRepository = repository;
            _mediatr = mediatr;
            _solicitacaoRepository = solicitacaoRepository;
        }

        public async Task Handle(SolicitacaoLocacaoAprovadaEvent message, CancellationToken cancellationToken)
        {
            Solicitacao solicitacaoParaAprovar = await _solicitacaoRepository.ObterPorId(message.SolicitacaoId);

            solicitacaoParaAprovar.Aprovar();

            Locacao novaLocacao = new Locacao(solicitacaoParaAprovar.Id, solicitacaoParaAprovar.UsuarioLocatarioId, solicitacaoParaAprovar.EmpresaId, solicitacaoParaAprovar.QuadraId,
                                                solicitacaoParaAprovar.TempoTotalLocacaoSolicitado, message.InformacoesTempoQuadra);

            _locacaoRepository.AdicionarLocacao(novaLocacao);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacaoParaAprovar);

            if (await _locacaoRepository.Commit())
                await _mediatr.Publish(new LocacaoCriadaEvent(novaLocacao.Id));
        }

        public async Task Handle(SolicitacaoLocacaoRecusadaEvent message, CancellationToken cancellationToken)
        {
            Solicitacao solicitacaoParaRecusar = await _solicitacaoRepository.ObterPorId(message.SolicitacaoId);

            solicitacaoParaRecusar.Recusar(message.Motivo);

            _solicitacaoRepository.AtualizarSolicitacao(solicitacaoParaRecusar);

            await _solicitacaoRepository.Commit();
        }

        public async Task Handle(SolicitacaoLocacaoCanceladaEvent message, CancellationToken cancellationToken)
        {
            Solicitacao solicitacaoParaCancelar = await _solicitacaoRepository.ObterPorId(message.SolicitacaoId);

            solicitacaoParaCancelar.Cancelar(message.MotivoCancelamento);

            _solicitacaoRepository.AtualizarSolicitacao(solicitacaoParaCancelar);

            Locacao locacaoParaCancelar = await _locacaoRepository.ObterPorSolicitacao(message.SolicitacaoId);

            locacaoParaCancelar.CancelarLocacao();

            _locacaoRepository.AtualizarLocacao(locacaoParaCancelar);

            await _solicitacaoRepository.Commit();
            await _locacaoRepository.Commit();
        }
    }
}
