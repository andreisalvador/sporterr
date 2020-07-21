using FluentValidation;
using System;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class EmpresaValidation : AbstractValidator<Empresa>
    {
        public EmpresaValidation()
        {
            RuleFor(e => e.RazaoSocial)
                .NotEmpty().WithMessage("A razão social precisa ser informada.")
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(e => e.Cnpj)
                .NotNull().WithMessage("O Cnpj precisa ser válido.");

            RuleFor(e => e.DiasFuncionamento)
                .IsInEnum().WithMessage("O registro de dias de funcionamento informado é inválido.");
        }
    }
}
