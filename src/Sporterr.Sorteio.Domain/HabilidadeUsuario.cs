using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Sorteio.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class HabilidadeUsuario : Entity<HabilidadeUsuario>, IAggregateRoot
    {
        private readonly List<AvaliacaoHabilidade> _avaliacoes;

        public Guid PerfilHabilidadesId { get; private set; }
        public Guid HabilidadeId { get; private set; }
        public Guid EsporteId { get; private set; }
        public IReadOnlyCollection<AvaliacaoHabilidade> Avaliacoes => _avaliacoes.AsReadOnly();
        public double Nota { get; private set; }

        public PerfilHabilidades PerfilHabilidades { get; set; }
        public Esporte Esporte { get; set; }
        public Habilidade Habilidade { get; set; }

        public HabilidadeUsuario(Guid habilidadeId, Guid esporteId)
        {
            HabilidadeId = habilidadeId;
            EsporteId = esporteId;            
            _avaliacoes = new List<AvaliacaoHabilidade>();
        }

        internal void AssociarPerfilHabilidadesUsuario(Guid perfilHabilidades) => PerfilHabilidadesId = perfilHabilidades;

        public void AdicionarAvaliacaoHabilidade(AvaliacaoHabilidade avaliacao)
        {
            avaliacao.AssociarHabilidadeUsuario(Id);
            _avaliacoes.Add(avaliacao);
            Nota = _avaliacoes.Average(a => a.Nota);
        }

        public override void Validate()
        {
            Validate(this, new HabilidadeUsuarioValidation());
        }
    }
}
