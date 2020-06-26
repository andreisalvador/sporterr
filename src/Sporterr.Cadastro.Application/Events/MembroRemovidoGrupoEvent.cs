using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Events
{
    public class MembroRemovidoGrupoEvent : Event
    {
        public Guid MembroRemovidoId { get; private set; }
        public Guid UsuarioMembroRemovidoId { get; private set; }
        public Guid GrupoId { get; private set; }

        public MembroRemovidoGrupoEvent(Guid membroRemovidoId, Guid usuarioMembroRemovidoId, Guid grupoId)
        {
            AggregateId = membroRemovidoId;
            MembroRemovidoId = membroRemovidoId;
            UsuarioMembroRemovidoId = usuarioMembroRemovidoId;
            GrupoId = grupoId;
        }
    }
}
