using Sporterr.Core.DomainObjects;
using Sporterr.Core.Enums;
using Sporterr.Sorteio.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class Habilidade : Entity<Habilidade>
    {
        public string Nome { get; private set; }
        public Guid EsporteId { get; private set; }

        public Esporte Esporte { get; private set; }
        public Habilidade(string nome)
        {
            Nome = nome;
        }

        internal void AssociarEsporte(Guid esporteId) => EsporteId = esporteId;

        public override void Validate()
        {
            Validate(this, new HabilidadeValidation());
        }
    }
}
