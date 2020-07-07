using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoCancelamentoLocacaoEnviadaEvent : IntegrationEvent
    {
        public Guid SolicitacaoId { get; private set; }        
        public Guid EmpresaId { get; private set; }
        public string MotivoCancelamento { get; private set; }

        public SolicitacaoCancelamentoLocacaoEnviadaEvent(Guid solicitacaoId, Guid empresaId, string motivoCancelamento = "Cancelamento solicitado pelo usuário sem motivo específico.")
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;                     
            EmpresaId = empresaId;
            MotivoCancelamento = motivoCancelamento;
        }
    }
}