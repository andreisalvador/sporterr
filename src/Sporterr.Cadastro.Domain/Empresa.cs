using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
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
        public Guid UsuarioProprietarioId { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; } //verificar dps
        public DiasSemanaFuncionamento DiasFuncionamento { get; private set; }
        public TimeSpan HorarioAbertura { get; private set; }
        public TimeSpan HorarioFechamento { get; private set; }
        public IReadOnlyCollection<Quadra> Quadras => _quadras.AsReadOnly();

        //Ef Rel.
        public Usuario UsuarioProprietario { get; set; }

        public Empresa(Guid usuarioProprietarioId,
                       string razaoSocial,
                       string cnpj,
                       TimeSpan horarioAbertura,
                       TimeSpan horarioFechamento,
                       DiasSemanaFuncionamento diasFuncionamento = DiasSemanaFuncionamento.DiasUteis)
        {

            UsuarioProprietarioId = usuarioProprietarioId;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            HorarioAbertura = horarioAbertura;
            HorarioFechamento = horarioFechamento;
            DiasFuncionamento = diasFuncionamento;
            _quadras = new List<Quadra>();
            Ativar();
            Validar();
        }

        public void AdicionarQuadra(Quadra quadra)
        {
            if (quadra.Validar() && !QuadraPertenceEmpresa(quadra)) _quadras.Add(quadra);
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
            if(!quadra.EmManutencao && QuadraPertenceEmpresa(quadra) && quadra.Validar())
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

        public bool QuadraPertenceEmpresa(Quadra quadra) => _quadras.Any(q => q.Equals(quadra));

        protected override AbstractValidator<Empresa> ObterValidador() => new EmpresaValidation();
    }
}
