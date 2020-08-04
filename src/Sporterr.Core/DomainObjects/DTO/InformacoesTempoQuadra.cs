using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.DomainObjects.DTO
{
    public struct InformacoesTempoQuadra
    {
        public decimal ValorPorTempoLocadoQuadra { get; private set; }
        public TimeSpan TempoLocacaoQuadra { get; private set; }
        public InformacoesTempoQuadra(decimal valorPorTempoLocadoQuadra, TimeSpan tempoLocacaoQuadra)
        {
            ValorPorTempoLocadoQuadra = valorPorTempoLocadoQuadra;
            TempoLocacaoQuadra = tempoLocacaoQuadra;
        }
    }
}
