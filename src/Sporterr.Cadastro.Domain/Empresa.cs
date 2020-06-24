﻿using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporterr.Cadastro.Domain
{
    public class Empresa : Entity<Empresa>, IAggregateRoot
    {
        private readonly List<Quadra> _quadras;
        private readonly List<Solicitacao> _solicitacoes;

        public Guid UsuarioProprietarioId { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; } //verificar dps
        public DiasSemanaFuncionamento DiasFuncionamento { get; private set; }
        public TimeSpan HorarioAbertura { get; private set; }
        public TimeSpan HorarioFechamento { get; private set; }
        public IReadOnlyCollection<Quadra> Quadras => _quadras.AsReadOnly();
        public IReadOnlyCollection<Solicitacao> Solicitacoes => _solicitacoes.AsReadOnly();

        //Ef Rel.
        public Usuario UsuarioProprietario { get; set; }

        public Empresa(string razaoSocial,
                       string cnpj,
                       TimeSpan horarioAbertura,
                       TimeSpan horarioFechamento,
                       DiasSemanaFuncionamento diasFuncionamento = DiasSemanaFuncionamento.DiasUteis)
        {
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            HorarioAbertura = horarioAbertura;
            HorarioFechamento = horarioFechamento;
            DiasFuncionamento = diasFuncionamento;
            _quadras = new List<Quadra>();
            _solicitacoes = new List<Solicitacao>();
            Ativar();
            Validar();
        }

        internal void AssociarUsuarioProprietario(Guid usuarioProprietarioId) => UsuarioProprietarioId = usuarioProprietarioId;

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            if (solicitacao.Validar() && !ExisteSolicitacaoParaEmpresa(solicitacao))
            {
                solicitacao.AssociarEmpresaSolicitacao(Id);
                _solicitacoes.Add(solicitacao);
            }
        }

        public void AprovarSolicitacao(Solicitacao solicitacao)
        {
            if (solicitacao.Validar() && ExisteSolicitacaoParaEmpresa(solicitacao))
            {
                Solicitacao solicitacaoParaAprovar = _solicitacoes.SingleOrDefault(s => s.Id.Equals(solicitacao.Id));
                solicitacaoParaAprovar.Aprovar();
            }
        }

        public void RecusarSolicitacao(Solicitacao solicitacao, string motivo)
        {
            if (solicitacao.Validar() && ExisteSolicitacaoParaEmpresa(solicitacao))
            {
                Solicitacao solicitacaoParaRecusar = _solicitacoes.SingleOrDefault(s => s.Id.Equals(solicitacao.Id));
                solicitacaoParaRecusar.Recusar(motivo);
            }
        }

        public void CancelarSolicitacao(Solicitacao solicitacao, string motivo)
        {
            if (solicitacao.Validar() && ExisteSolicitacaoParaEmpresa(solicitacao))
            {
                Solicitacao solicitacaoParaRecusar = _solicitacoes.SingleOrDefault(s => s.Id.Equals(solicitacao.Id));
                solicitacaoParaRecusar.Cancelar(motivo);
            }
        }


        private bool ExisteSolicitacaoParaEmpresa(Solicitacao solicitacao) => _solicitacoes.Any(s => s.EmpresaId.Equals(Id) && s.Id.Equals(solicitacao.Id));

        public void AdicionarQuadra(Quadra quadra)
        {
            if (quadra.Validar() && !QuadraPertenceEmpresa(quadra))
            {
                quadra.AssociarEmpresaProprietaria(Id);
                _quadras.Add(quadra);
            }
        }

        public void InativarQuadra(Quadra quadra)
        {
            if (quadra.Validar() && quadra.Ativo && QuadraPertenceEmpresa(quadra))
            {
                Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
                quadraExistente.Inativar();
            }
        }

        public void ReativarQuadra(Quadra quadra)
        {
            if (quadra.Validar() && !quadra.Ativo && QuadraPertenceEmpresa(quadra))
            {
                Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
                quadraExistente.Ativar();
            }
        }

        public void AlterarHorarioAbertura(TimeSpan horarioAbertura)
        {
            if (HorarioAbertura != horarioAbertura) HorarioAbertura = horarioAbertura;
        }

        public void AlterarHorarioFechamento(TimeSpan horarioFechamento)
        {
            if (HorarioFechamento != horarioFechamento) HorarioAbertura = horarioFechamento;
        }

        public void AlterarHorarioFuncionamento(TimeSpan horarioAbertura, TimeSpan horarioFechamento)
        {
            AlterarHorarioAbertura(horarioAbertura);
            AlterarHorarioFechamento(horarioFechamento);
        }

        public void AtivarFuncionamentoNoDiaDaSemana(DiasSemanaFuncionamento diasSemanaFuncionamento)
        {
            DiasFuncionamento |= diasSemanaFuncionamento;
        }

        public void DesativarFuncionamentoNoDiaDaSemana(DiasSemanaFuncionamento diasSemanaFuncionamento)
        {
            DiasFuncionamento &= ~diasSemanaFuncionamento;
        }

        public void AlterarDiasFuncionamento(DiasSemanaFuncionamento diasFuncionamento)
        {
            DiasFuncionamento = diasFuncionamento;
        }

        public void ColocarQuadraEmManutencao(Quadra quadra)
        {
            if (!quadra.EmManutencao && QuadraPertenceEmpresa(quadra) && quadra.Validar())
            {
                Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
                quadra.ColocarQuadraEmManutencao();
            }
        }

        public void RetirarQuadraDeManutencao(Quadra quadra)
        {
            if (quadra.EmManutencao && QuadraPertenceEmpresa(quadra) && quadra.Validar())
            {
                Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
                quadra.TornarQuadraProntaPraUso();
            }
        }

        public bool PossuiQuadras() => _quadras.Any();
        public bool PossuiSolicitacoesPendentes() => _solicitacoes.Any(s => s.EstaPendente());

        public bool QuadraPertenceEmpresa(Quadra quadra) => _quadras.Any(q => q.Equals(quadra));

        public override void Inativar()
        {
            if (PossuiSolicitacoesPendentes())
                throw new DomainException("Não é possível inativar empresas com solicitações pendentes");

            base.Inativar();
        }

        protected override AbstractValidator<Empresa> ObterValidador() => new EmpresaValidation();
    }
}
