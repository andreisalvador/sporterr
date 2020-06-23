using Sporterr.Core.Enums;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{ 
    public class SolicitacaoAdicionadaEvent : Event
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public SolicitacaoAdicionadaEvent(Guid solicitacaoId, Guid empresaId, Guid quadraId, StatusSolicitacao status)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = status;
        }
    }
}