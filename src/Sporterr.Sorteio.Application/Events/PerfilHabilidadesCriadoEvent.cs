using Sporterr.Core.Messages;
using System;

namespace Sporterr.Sorteio.Application.Events
{
    public class PerfilHabilidadesCriadoEvent : Event
    {
        public Guid PerfilHabilidadesId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public PerfilHabilidadesCriadoEvent(Guid perfilHabilidadesId, Guid usuarioId)
        {
            AggregateId = perfilHabilidadesId;
            PerfilHabilidadesId = perfilHabilidadesId;
            UsuarioId = usuarioId;
        }
    }
}
