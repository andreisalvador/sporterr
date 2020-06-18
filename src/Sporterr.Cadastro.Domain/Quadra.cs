using FluentValidation;
using Sporterr.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain
{
    public class Quadra : Entity<Quadra>
    {
        public Guid EmpresaId { get; private set; }
        public decimal ValorTempoLocado { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public bool EmManutencao { get; private set; }

        // Ef rel.
        public Empresa Empresa { get; set; }

        public Quadra(Guid empresaId, TimeSpan tempoLocacao, decimal valorTempoLocado)
        {
            EmpresaId = empresaId;
            TempoLocacao = tempoLocacao;
            ValorTempoLocado = valorTempoLocado;
            TornarQuadraProntaPraUso();
            Validar();
        }

        internal void ColocarQuadraEmManutencao() => EmManutencao = true;
        internal void TornarQuadraProntaPraUso() => EmManutencao = false;

        protected override AbstractValidator<Quadra> ObterValidador()
        {
            throw new NotImplementedException();
        }
    }
}
