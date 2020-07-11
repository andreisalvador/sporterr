using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class Esporte : Entity<Esporte>, IAggregateRoot
    {
        private readonly IList<Habilidade> _habilidades;

        public string Nome { get; private set; }
        public Core.Enums.Esportes TipoEsporte { get; }
        public IReadOnlyCollection<Habilidade> Habilidades { get; private set; }

        public Esporte(string nome, Core.Enums.Esportes tipoEsporte)
        {
            Nome = nome;
            TipoEsporte = tipoEsporte;
            _habilidades = new List<Habilidade>();
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
            throw new NotImplementedException();
        }
    }
}
