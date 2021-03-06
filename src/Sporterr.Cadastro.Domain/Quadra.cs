﻿using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Enums;
using System;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Quadra : Entity<Quadra>, IActivationEntity
    {
        public Guid EmpresaId { get; private set; }
        public decimal ValorPorTempoLocado { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public bool EmManutencao { get; private set; }
        public TipoEsporte TipoEsporteQuadra { get; private set; }
        public bool Ativo { get; private set; }

        // Ef rel.
        public Empresa Empresa { get; set; }

        public Quadra(TipoEsporte tipoEsporteQuadra, TimeSpan tempoLocacao, decimal valorPorTempoLocado)
        {            
            TempoLocacao = tempoLocacao;
            ValorPorTempoLocado = valorPorTempoLocado;
            TipoEsporteQuadra = tipoEsporteQuadra;
            TornarQuadraProntaPraUso();
            Ativar();
            Validate();
        }

        internal void AssociarEmpresaProprietaria(Guid empresaProprietariaId) => EmpresaId = empresaProprietariaId;

        public void ColocarQuadraEmManutencao()
        {
            //if (PossuiSolicitacaoDeLocacaoPendente())
            //    throw new DomainException("Não é possível colocar uma quadra em manutenção com processos de locação pendentes.");

            EmManutencao = true;
        }

        public void TornarQuadraProntaPraUso() => EmManutencao = false;
    

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            //if (PossuiSolicitacaoDeLocacaoPendente())
            //    throw new DomainException("Não é possível inativar uma quadra com processos de locação pendentes.");

            Ativo = false;
        }

        public override void Validate() => Validate(this, new QuadraValidation());
    }
}
