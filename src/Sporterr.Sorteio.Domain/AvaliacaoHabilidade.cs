using Sporterr.Core.DomainObjects;
using Sporterr.Sorteio.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class AvaliacaoHabilidade : Entity<AvaliacaoHabilidade>
    {
        public Guid UsuarioAvaliadoId { get; private set; }        
        public Guid HabilidadeUsuarioId { get; private set; }        
        public double Nota { get; private set; }

        public HabilidadeUsuario HabilidadeUsuario { get; set; }

        public AvaliacaoHabilidade(Guid usuarioAvaliadoId, double nota = 0)
        {
            UsuarioAvaliadoId = usuarioAvaliadoId;            
            Nota = nota;
        }

        internal void AssociarHabilidadeUsuario(Guid habilidadeUsuaioId) => HabilidadeUsuarioId = habilidadeUsuaioId;

        public override void Validate()
        {
            Validate(this, new AvaliacaoHabilidadeValidation());
        }
    }
}
