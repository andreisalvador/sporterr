using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Empresa : Entity<Empresa>, IActivationEntity, IAggregateRoot
    {
        private readonly List<Quadra> _quadras;

        public Guid UsuarioProprietarioId { get; private set; }
        public string RazaoSocial { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public DiasSemanaFuncionamento DiasFuncionamento { get; private set; }
        public TimeSpan HorarioAbertura { get; private set; }
        public TimeSpan HorarioFechamento { get; private set; }
        public bool Ativo { get; private set; }
        public IReadOnlyCollection<Quadra> Quadras => _quadras.AsReadOnly();
        //Ef Rel.
        public Usuario UsuarioProprietario { get; set; }

        //Ef core
        protected Empresa() { }

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
            Ativar();
            Validate();
        }

        internal void AssociarUsuarioProprietario(Guid usuarioProprietarioId) => UsuarioProprietarioId = usuarioProprietarioId;

        public void AdicionarQuadra(Quadra quadra)
        {
            quadra.Validate();

            if (QuadraPertenceEmpresa(quadra))
                throw new DomainException($"A quadra informada ja pertence a empresa '{RazaoSocial}'");

            quadra.AssociarEmpresaProprietaria(Id);
            _quadras.Add(quadra);
        }

        public void InativarQuadra(Quadra quadra)
        {
            quadra.Validate();

            if (!quadra.Ativo)
                throw new DomainException("A quadra informada ja está inativa.");

            ValidarSeQuadraPertenceEmpresa(quadra);

            Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
            quadraExistente.Inativar();

        }

        public void ReativarQuadra(Quadra quadra)
        {
            quadra.Validate();

            if (quadra.Ativo)
                throw new DomainException("A quadra informada ja está ativa.");

            ValidarSeQuadraPertenceEmpresa(quadra);

            Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
            quadraExistente.Ativar();
        }

        public void AlterarHorarioAbertura(TimeSpan horarioAbertura)
        {
            if (HorarioAbertura != horarioAbertura) HorarioAbertura = horarioAbertura;
        }

        public void AlterarHorarioFechamento(TimeSpan horarioFechamento)
        {
            if (HorarioFechamento != horarioFechamento) HorarioFechamento = horarioFechamento;
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
            quadra.Validate();

            if (quadra.EmManutencao)
                throw new DomainException("A quadra já se encontra em manutenção.");

            ValidarSeQuadraPertenceEmpresa(quadra);

            Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
            quadra.ColocarQuadraEmManutencao();
        }

        public void RetirarQuadraDeManutencao(Quadra quadra)
        {
            quadra.Validate();

            if (!quadra.EmManutencao)
                throw new DomainException("A quadra não se encontra em manutenção.");

            ValidarSeQuadraPertenceEmpresa(quadra);

            Quadra quadraExistente = _quadras.SingleOrDefault(q => q.Id.Equals(quadra.Id));
            quadra.TornarQuadraProntaPraUso();
        }

        public bool PossuiQuadras() => _quadras.Count > 0;
        public bool QuadraPertenceEmpresa(Quadra quadra) => _quadras.Any(q => q.Equals(quadra));

        private void ValidarSeQuadraPertenceEmpresa(Quadra quadra)
        {
            if (!QuadraPertenceEmpresa(quadra))
                throw new DomainException($"A quadra informada não pertence a empresa '{RazaoSocial}'.");
        }
        public void Ativar()
        {
            Ativo = true;
        }
        public void Inativar()
        {
            //if (PossuiSolicitacoesPendentes())
            //    throw new DomainException("Não é possível inativar empresas com solicitações pendentes");

            Ativo = false;
        }

        public override void Validate() => Validate(this, new EmpresaValidation());

    }
}
