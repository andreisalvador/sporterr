using Sporterr.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class HabilidadeUsuario : Entity<HabilidadeUsuario>
    {
        public Guid UsuarioId { get; private set; }
        public Guid HabilidadeId { get; private set; }
        public Guid EsporteId { get; private set; }
        public sbyte Nota { get; private set; }

        public Esporte Esporte { get; set; }
        public Habilidade Habilidade { get; set; }
        public HabilidadeUsuario(Guid usuarioId, Guid habilidadeId, Guid esporteId, sbyte nota = 0)
        {
            UsuarioId = usuarioId;
            HabilidadeId = habilidadeId;
            EsporteId = esporteId;
            Nota = nota;
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
