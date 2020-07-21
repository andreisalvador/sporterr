using FluentValidation.Results;
using MediatR;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Core.Messages.Handler;
using Sporterr.Locacoes.Domain;
using Sporterr.Locacoes.Domain.Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Application.Commands.Handlers
{
    public class SolicitacaoCommandHandler : CommandHandler<Locacao>,
        IRequestHandler<AbrirSolicitacaoLocacaoCommand, ValidationResult>,
        IRequestHandler<SolicitarCancelamentoLocacaoCommand, ValidationResult>
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IMediatrHandler _mediatr;
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        public SolicitacaoCommandHandler(ISolicitacaoRepository solicitacaoRepository, ILocacaoRepository repository, IMediatrHandler mediatr) : base(repository, mediatr)
        {
            _locacaoRepository = repository;
            _mediatr = mediatr;
            _solicitacaoRepository = solicitacaoRepository;
        }
        
        public async Task<ValidationResult> Handle(AbrirSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            bool solicitacaoExistenteNoPeriodo = await _solicitacaoRepository.ExisteNoPeriodo(message.DataHoraInicioLocacao, message.DataHoraFimLocacao);

            if (solicitacaoExistenteNoPeriodo) return await NotifyAndReturn("O período informado já foi locado, favor escolher outro período.");

            Solicitacao novaSolicitacao = new Solicitacao(message.UsuarioLocatarioId, message.EmpresaId, message.QuadraId, message.DataHoraInicioLocacao, message.DataHoraFimLocacao);

            _solicitacaoRepository.AdicionarSolicitacao(novaSolicitacao);

            return await SaveAndPublish(new SolicitacaoLocacaoEnviadaEvent(novaSolicitacao.Id, novaSolicitacao.UsuarioLocatarioId, novaSolicitacao.EmpresaId,
                                        novaSolicitacao.QuadraId, message.DataHoraInicioLocacao, message.DataHoraFimLocacao));
                                        
        }

        public async Task<ValidationResult> Handle(SolicitarCancelamentoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            Solicitacao solicitacaoParaCancelar = await _solicitacaoRepository.ObterPorId(message.SolicitacaoId);

            if (solicitacaoParaCancelar == null) return await NotifyAndReturn("Solicitação não encontrada.");

            solicitacaoParaCancelar.AguardarCancelamento();
            
            _solicitacaoRepository.AtualizarSolicitacao(solicitacaoParaCancelar);            

            return await SaveAndPublish(new SolicitacaoCancelamentoLocacaoEnviadaEvent(solicitacaoParaCancelar.Id, solicitacaoParaCancelar.EmpresaId));
        }
    }
}
