using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoAprovadaEvent : IntegrationEvent
    {
        public Guid LocacaoId { get; private set; }
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public SolicitacaoLocacaoAprovadaEvent(Guid solicitacaoId, Guid empresaId, Guid locacaoId)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            LocacaoId = locacaoId;
        }
    }
}
