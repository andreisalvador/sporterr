using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoAprovadaEvent : IntegrationEvent
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public SolicitacaoLocacaoAprovadaEvent(Guid empresaId, Guid locacaoId)
        {
            AggregateId = empresaId;
            EmpresaId = empresaId;
            LocacaoId = locacaoId;
        }
    }
}
