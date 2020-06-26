using FluentValidation;
using System;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class EmpresaValidation : AbstractValidator<Empresa>
    {
        public EmpresaValidation()
        {
            RuleFor(e => e.Cnpj)
                .NotEmpty();

            RuleFor(e => e.DiasFuncionamento)
                .IsInEnum();

            RuleFor(e => e.RazaoSocial)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100);
        }
    }
}
