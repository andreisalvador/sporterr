using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoRecusadaEvent : IntegrationEvent
    {
        public Guid LocacaoId { get; private set; }
        public Guid SolicitacaoId { get; private set; }
        public SolicitacaoLocacaoRecusadaEvent(Guid solicitacaoId, Guid locacaoId)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            LocacaoId = locacaoId;
        }
    }
}
