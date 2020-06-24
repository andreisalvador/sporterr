using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class CancelamentoLocacaoSolicitadoEvent : IntegrationEvent
    {
        public Guid LocacaoId { get; private set; }      
        public Guid EmpresaId { get; private set; }
        public string MotivoCancelamento { get; private set; }

        public CancelamentoLocacaoSolicitadoEvent(Guid locacaoId, Guid empresaId, string motivoCancelamento)
        {
            AggregateId = locacaoId;
            LocacaoId = locacaoId;            
            EmpresaId = empresaId;
            MotivoCancelamento = motivoCancelamento;
        }
    }
}