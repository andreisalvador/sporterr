using FluentValidation;
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
            Validar();
        }

        internal void AdicionarQuadra(Quadra quadra)
        {
            if (!QuadraPertenceEmpresa(quadra)) _quadras.Add(quadra);
        }

        internal void RemoverQuadra(Quadra quadra)
        {
            if (QuadraPertenceEmpresa(quadra)) _quadras.Remove(quadra);
        }

        public bool QuadraPertenceEmpresa(Quadra quadra) => _quadras.Any(q => q.Equals(quadra));

        protected override AbstractValidator<Empresa> ObterValidador()
        {
            throw new NotImplementedException();
        }
    }
}
