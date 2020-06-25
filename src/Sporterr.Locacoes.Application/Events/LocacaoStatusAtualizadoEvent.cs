using Sporterr.Core.Messages;
using Sporterr.Locacoes.Domain.Enums;
using System;

namespace Sporterr.Locacoes.Application.Events
{
    public class LocacaoStatusAtualizadoEvent : Event
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusLocacao Status { get; private set; }
        public LocacaoStatusAtualizadoEvent(Guid locacaoId, Guid empresaId, Guid quadraId, StatusLocacao status)
        {
            AggregateId = locacaoId;
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = status;
        }        
    }
}
