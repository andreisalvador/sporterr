using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoRecusadaEvent : IntegrationEvent
    {
        public Guid SolicitacaoId { get; private set; }
        public string Motivo { get; private set; }
        public SolicitacaoLocacaoRecusadaEvent(Guid solicitacaoId, string motivo)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            Motivo = motivo;
        }
    }
}
