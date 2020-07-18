using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Sorteio.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class Esporte : Entity<Esporte>, IAggregateRoot
    {
        private readonly List<Habilidade> _habilidades;

        public string Nome { get; private set; }
        public Core.Enums.TipoEsporte TipoEsporte { get; }
        public IReadOnlyCollection<Habilidade> Habilidades => _habilidades.AsReadOnly();

        public Esporte(string nome, Core.Enums.TipoEsporte tipoEsporte)
        {
            Nome = nome;
            TipoEsporte = tipoEsporte;
            _habilidades = new List<Habilidade>();
            Validate();
        }

        public void AdicionarHabilidade(Habilidade habilidade)
        {
            habilidade.AssociarEsporte(Id);
            _habilidades.Add(habilidade);
        }

        public void RemoverHablidade(Habilidade habilidade)
        {
            _habilidades.Remove(habilidade);
        }

        public override void Validate()
        {
            Validate(this, new EsporteValidation());
        }
    }
}
