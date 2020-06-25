using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Events
{
    public class QuadraInativadaEmpresaEvent : Event
    {
        public Guid QuadraId { get; private set; }
        public Guid EmpresaId { get; private set; }

        public QuadraInativadaEmpresaEvent(Guid quadraId, Guid empresaId)
        {
            AggregateId = empresaId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }
    }
}
