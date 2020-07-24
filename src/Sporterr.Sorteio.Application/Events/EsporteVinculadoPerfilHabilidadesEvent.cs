using Sporterr.Core.Messages;
using System;

namespace Sporterr.Sorteio.Application.Events
{
    internal class EsporteVinculadoPerfilHabilidadesEvent : Event
    {
        public Guid PerfilHabilidadesId { get; private set; }
        public Guid EsporteId { get; private set; }

        public EsporteVinculadoPerfilHabilidadesEvent(Guid perfilHabilidadesId, Guid esporteId)
        {
            AggregateId = perfilHabilidadesId;
            PerfilHabilidadesId = perfilHabilidadesId;
            EsporteId = esporteId;
        }
    }
}