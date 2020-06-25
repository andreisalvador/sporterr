using Sporterr.Core.DomainObjects.Interfaces;
using System;

namespace Sporterr.Locacoes.Domain
{
    public class Quadra : IValueObject
    {
        public Guid Id { get; private set; }
        public decimal ValorTempoQuadra { get; private set; }
        public TimeSpan TempoLocacaoQuadra { get; private set; }
        public Quadra(Guid id, decimal valorTempoQuadra, TimeSpan tempoLocacaoQuadra)
        {
            Id = id;
            ValorTempoQuadra = valorTempoQuadra;
            TempoLocacaoQuadra = tempoLocacaoQuadra;
        }
    }
}
