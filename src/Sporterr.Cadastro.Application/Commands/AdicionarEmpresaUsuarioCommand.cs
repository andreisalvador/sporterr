using FluentValidation;
using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarEmpresaUsuarioCommand : Command<AdicionarEmpresaUsuarioCommand>
    {
        public Guid UsuarioProprietarioId { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; } //verificar dps
        public DiasSemanaFuncionamento DiasFuncionamento { get; private set; }
        public TimeSpan HorarioAbertura { get; private set; }
        public TimeSpan HorarioFechamento { get; private set; }

        public AdicionarEmpresaUsuarioCommand(Guid usuarioProprietarioId, string razaoSocial, string cnpj, DiasSemanaFuncionamento diasFuncionamento, TimeSpan horarioAbertura, TimeSpan horarioFechamento)
            : base(new AdicionarEmpresaUsuarioValidation())
        {
            UsuarioProprietarioId = usuarioProprietarioId;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            DiasFuncionamento = diasFuncionamento;
            HorarioAbertura = horarioAbertura;
            HorarioFechamento = horarioFechamento;
        }        

        private class AdicionarEmpresaUsuarioValidation : AbstractValidator<AdicionarEmpresaUsuarioCommand>
        {
            public AdicionarEmpresaUsuarioValidation()
            {
                RuleFor(e => e.UsuarioProprietarioId)
                    .NotEqual(Guid.Empty).WithMessage("O id do usuário proprietário não pode ser vázio.");

                RuleFor(e => e.RazaoSocial)
                    .NotEmpty().WithMessage("A razão social da empresa não pode ser vázio.");

                RuleFor(e => e.Cnpj)
                    .NotEmpty().WithMessage("O CNPJ da empresa não pode ser vázio.");

                RuleFor(e => e.DiasFuncionamento)
                    .IsInEnum().WithMessage("O(s) dia(s) da semana informado não é válido.")
                    .When(x => (int)x.DiasFuncionamento > (int)DiasSemanaFuncionamento.Todos).WithMessage("O(s) dia(s) da semana informado não é válido.");

                RuleFor(e => e.HorarioAbertura)
                    .NotEqual(TimeSpan.MinValue).WithMessage("O horário de abertura precisa ser informado.");

                RuleFor(e => e.HorarioFechamento)
                .NotEqual(TimeSpan.MinValue).WithMessage("O horário de fechamento precisa ser informado.");
            }
        }
    }
}
