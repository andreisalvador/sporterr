using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacao.Domain
{
    public class Quadra : IValueObject
    {
        public Guid Id { get; private set; }
        public Guid EmpresaId { get; private set; }
        public decimal ValorTempoLocado { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }

        public Quadra(Guid quadraId Guid empresaId, decimal valorTempoLocado, TimeSpan tempoLocacao)
        {
            Id = quadraId;
            EmpresaId = empresaId;
            ValorTempoLocado = valorTempoLocado;
            TempoLocacao = tempoLocacao;
        }
    }
}
