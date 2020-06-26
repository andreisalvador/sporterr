using Sporterr.Core.DomainObjects.Interfaces;
using System;

namespace Sporterr.Locacoes.Domain
{
    public class Quadra : IValueObject
    {
        public Guid Id { get; private set; }
        public decimal ValorPorTempoLocadoQuadra { get; private set; }
        public TimeSpan TempoLocacaoQuadra { get; private set; }

        protected Quadra() { /*Ef core*/ }
        public Quadra(Guid id, decimal valorTempoQuadra, TimeSpan tempoLocacaoQuadra)
        {
            Id = id;
            ValorPorTempoLocadoQuadra = valorTempoQuadra;
            TempoLocacaoQuadra = tempoLocacaoQuadra;
        }
    }
}
