using Sporterr.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain
{
    public class Quadra : Entity
    {
        public Guid EmpresaId { get; private set; }
        public decimal ValorLocacao { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public bool EmManutencao { get; private set; }

        // Ef rel.
        public Empresa Empresa { get; set; }

        public Quadra(Guid empresaId, TimeSpan tempoLocacao, decimal valorLocacao)
        {
            EmpresaId = empresaId;
            TempoLocacao = tempoLocacao;
            ValorLocacao = valorLocacao;
            TornarQuadraProntaPraUso();
        }

        internal void ColocarQuadraEmManutencao() => EmManutencao = true;
        internal void TornarQuadraProntaPraUso() => EmManutencao = false;
    }
}
