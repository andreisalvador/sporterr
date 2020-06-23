using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.Enums;
using System;

namespace Sporterr.Cadastro.Domain
{
    public class Quadra : Entity<Quadra>
    {
        public Guid EmpresaId { get; private set; }
        public decimal ValorTempoLocado { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public bool EmManutencao { get; private set; }
        public Esporte TipoEsporteQuadra { get; private set; }
        //adicionar esporte

        // Ef rel.
        public Empresa Empresa { get; set; }

        public Quadra(Guid empresaId, TimeSpan tempoLocacao, decimal valorTempoLocado, Esporte tipoEsporteQuadra)
        {
            EmpresaId = empresaId;
            TempoLocacao = tempoLocacao;
            ValorTempoLocado = valorTempoLocado;
            TipoEsporteQuadra = tipoEsporteQuadra;
            TornarQuadraProntaPraUso();
            Ativar();
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
