using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.DomainObjects.DTO
{
    public struct InformacoesTempoQuadra
    {
        public decimal ValorPorTempoLocadoQuadra { get; set; }
        public TimeSpan TempoLocacaoQuadra { get; set; }
    }
}
