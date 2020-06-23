using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoRecusadaEvent : IntegrationEvent
    {
        public Guid LocacaoId { get; private set; }
        public SolicitacaoLocacaoRecusadaEvent(Guid locacaoId)
        {
            AggregateId = locacaoId;
            LocacaoId = locacaoId;
        }
    }
}
