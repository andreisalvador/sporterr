using Sporterr.Core.Enums;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{ 
    public class SolicitacaoAbertaEvent : IntegrationEvent
    {
        public Guid LocaocaId { get; private set; }
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public SolicitacaoAbertaEvent(Guid locacaoId, Guid solicitacaoId, Guid empresaId, Guid quadraId, StatusSolicitacao status)
        {
            AggregateId = solicitacaoId;
            LocaocaId = locacaoId;
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = status;
        }
    }
}