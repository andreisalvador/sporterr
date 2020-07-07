using Sporterr.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoAprovadaEvent : IntegrationEvent
    {   
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public InformacoesTempoQuadra InformacoesTempoQuadra { get; private set; }
        public SolicitacaoLocacaoAprovadaEvent(Guid solicitacaoId, Guid empresaId, Guid quadraId, InformacoesTempoQuadra informacoesTempoQuadra)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            InformacoesTempoQuadra = informacoesTempoQuadra;
        }
    }
}
