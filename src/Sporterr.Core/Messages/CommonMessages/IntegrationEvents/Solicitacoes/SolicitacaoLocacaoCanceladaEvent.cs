using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoCanceladaEvent : Event
    {
        public Guid SolicitacaoId { get; private set; }
        public StatusSolicitacao StatusSolicitacao { get; private set; }        
        public string MotivoCancelamento { get; private set; }
        public SolicitacaoLocacaoCanceladaEvent(Guid solicitacaoId, string movitoCancelamento)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            MotivoCancelamento = movitoCancelamento;
            StatusSolicitacao = StatusSolicitacao.Cancelada;
        }
    }
}
