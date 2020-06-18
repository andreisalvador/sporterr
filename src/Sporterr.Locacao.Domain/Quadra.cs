using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain
{
    public class Quadra : IValueObject
    {
        public Guid Id { get; private set; }
        public Guid EmpresaId { get; private set; }
        public decimal ValorTempoQuadra { get; private set; }
        public TimeSpan TempoLocacaoQuadra { get; private set; }
    }
}
