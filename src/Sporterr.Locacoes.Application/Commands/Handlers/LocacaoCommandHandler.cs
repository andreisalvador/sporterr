using MediatR;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Core.Messages.Handler;
using Sporterr.Locacoes.Application.Events;
using Sporterr.Locacoes.Data.Repository.Interfaces;
using Sporterr.Locacoes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Application.Commands.Handlers
{
    public class LocacaoCommandHandler : BaseCommandHandler<Locacao>,
        IRequestHandler<AbrirSolicitacaoLocacaoCommand, bool>,
        IRequestHandler<SolicitarCancelamentoLocacaoCommand, bool>
    {
        private readonly ILocacaoRepository _repository;
        private readonly IMediatrHandler _mediatr;
        public LocacaoCommandHandler(ILocacaoRepository repository, IMediatrHandler mediatr) : base(repository, mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
        }
        
        public async Task<bool> Handle(AbrirSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            bool locacaoExistenteNoPeriodo = await _repository.ExisteNoPeriodo(message.DataHoraInicioLocacao, message.DataHoraFimLocacao);

            if (locacaoExistenteNoPeriodo) return await NotifyAndReturn("O período informado já foi locado, favor escolher outro período.");            

            Locacao novaLocacao = new Locacao(message.UsuarioLocatarioId, message.EmpresaId, new Quadra(message.QuadraId, message.ValorTempoQuadra, message.TempoLocacaoQuadra), 
                                                                                                    message.DataHoraInicioLocacao, message.DataHoraFimLocacao);
            
            _repository.AdicionarLocacao(novaLocacao);

            return await SaveAndPublish(new LocacaoSolicitadaEvent(message.UsuarioLocatarioId, novaLocacao.Id, novaLocacao.Quadra.Id,
                                        novaLocacao.DataHoraInicioLocacao, novaLocacao.DataHoraFimLocacao, novaLocacao.Valor),
                                        new LocacaoStatusAtualizadoEvent(novaLocacao.Id, novaLocacao.EmpresaId, novaLocacao.Quadra.Id, novaLocacao.Status)); 
        }

        public async Task<bool> Handle(SolicitarCancelamentoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Locacao locacaoParaSolicitarCancelamento = await _repository.ObterPorId(message.LocacaoId);

            if (locacaoParaSolicitarCancelamento == null) return await NotifyAndReturn("Locação não encontarada.");

            locacaoParaSolicitarCancelamento.AguardarCancelamento();

            _repository.AtualizarLocacao(locacaoParaSolicitarCancelamento);

            return await SaveAndPublish(new CancelamentoLocacaoSolicitadoEvent(locacaoParaSolicitarCancelamento.Id, locacaoParaSolicitarCancelamento.EmpresaId, message.Motivo));
        }
    }
}
