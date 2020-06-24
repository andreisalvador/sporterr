using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoCanceladaEvent : Event
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public SolicitacaoLocacaoCanceladaEvent(Guid solicitacaoId, Guid locacaoId)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            LocacaoId = locacaoId;
        }

    }
}
