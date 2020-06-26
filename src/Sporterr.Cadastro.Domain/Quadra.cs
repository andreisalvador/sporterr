using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.Enums;
using System;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Quadra : Entity<Quadra>
    {
        public Guid EmpresaId { get; private set; }
        public decimal ValorPorTempoLocado { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public bool EmManutencao { get; private set; }
        public Esporte TipoEsporteQuadra { get; private set; }        

        // Ef rel.
        public Empresa Empresa { get; set; }

        public Quadra(Esporte tipoEsporteQuadra, TimeSpan tempoLocacao, decimal valorPorTempoLocado)
        {            
            TempoLocacao = tempoLocacao;
            ValorPorTempoLocado = valorPorTempoLocado;
            TipoEsporteQuadra = tipoEsporteQuadra;
            TornarQuadraProntaPraUso();
            Ativar();
            Validar();
        }

        internal void AssociarEmpresaProprietaria(Guid empresaProprietariaId) => EmpresaId = empresaProprietariaId;

        public void ColocarQuadraEmManutencao()
        {
            if (PossuiSolicitacaoDeLocacaoPendente())
                throw new DomainException("Não é possível colocar uma quadra em manutenção com processos de locação pendentes.");

            EmManutencao = true;
        }

        public void TornarQuadraProntaPraUso() => EmManutencao = false;

        public bool JaPassouPorProcessoLocacao() => Empresa.Solicitacoes.Any(s => s.QuadraId.Equals(Id));

        public bool PossuiSolicitacaoDeLocacaoPendente() =>
            Empresa.Solicitacoes.Any(s => s.QuadraId.Equals(Id) && s.EstaPendente());

        public override void Inativar()
        {
            if (PossuiSolicitacaoDeLocacaoPendente())
                throw new DomainException("Não é possível inativar uma quadra com processos de locação pendentes.");

            base.Inativar();
        }

        protected override AbstractValidator<Quadra> ObterValidador() => new QuadraValidation();
    }
}
