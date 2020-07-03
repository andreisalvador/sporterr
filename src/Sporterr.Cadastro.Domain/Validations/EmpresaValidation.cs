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
                .NotEmpty().WithMessage("O Cnpj precisa ser informado.");

            RuleFor(e => e.DiasFuncionamento)
                .IsInEnum().WithMessage("O registro de dias de funcionamento informado é inválido.");
        }
    }
}
