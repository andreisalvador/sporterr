using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{ 
    public class LocacaoSolicitadaEvent : IntegrationEvent
    {
        public Guid UsuarioLocatarioId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }
        public decimal Valor { get; private set; }
        public StatusSolicitacao Status { get; private set; }

        public LocacaoSolicitadaEvent(Guid usuarioLocatarioId, Guid locacaoId, Guid quadraId, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao, decimal valor)
        {
            AggregateId = locacaoId;
            UsuarioLocatarioId = usuarioLocatarioId;
            LocacaoId = locacaoId;
            QuadraId = quadraId;
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Valor = valor;
            Status = StatusSolicitacao.AguardandoAprovacao;
        }
    }
}