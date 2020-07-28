using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;

namespace Sporterr.Sorteio.Application.Events
{
    public class HabilidadeUsuarioAvaliadaEvent : Event
    {
        public Guid HabilidadeUsuarioId { get; private set; }
        public double Nota { get; private set; }

        public HabilidadeUsuarioAvaliadaEvent(Guid habilidadeUsuarioId, double nota)
        {
            AggregateId = habilidadeUsuarioId;
            HabilidadeUsuarioId = habilidadeUsuarioId;
            Nota = nota;
        }
    }
}