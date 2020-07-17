using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.DomainObjects.DTO
{
    public struct InformacoesSorteio
    {
        public IReadOnlyCollection<Guid> UsuariosParticipantes { get; private set; }
        public TipoEsporte Esporte { get; private set; }

        public InformacoesSorteio(List<Guid> usuariosPatricipantes, TipoEsporte esporte)
        {
            UsuariosParticipantes = usuariosPatricipantes.AsReadOnly();
            Esporte = esporte;
        }
    }
}
