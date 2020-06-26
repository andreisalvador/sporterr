using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Events
{
    public class MembroAdicionadoGrupoEvent : Event
    {
        public Guid MembroId { get; private set; }
        public Guid NovoMembroUsuarioId { get; private set; }
        public Guid GrupoId { get; private set; }
        public MembroAdicionadoGrupoEvent(Guid membroId, Guid novoMembroUsuarioId, Guid grupoId)
        {
            AggregateId = membroId;
            MembroId = membroId;
            NovoMembroUsuarioId = novoMembroUsuarioId;
            GrupoId = grupoId;
        }
    }
}
