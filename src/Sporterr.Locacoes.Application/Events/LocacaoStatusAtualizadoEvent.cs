using Sporterr.Core.Messages;
using Sporterr.Locacoes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = status;
        }        
    }
}
