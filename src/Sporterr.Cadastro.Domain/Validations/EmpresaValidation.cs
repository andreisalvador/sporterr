using FluentValidation;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class EmpresaValidation : AbstractValidator<Empresa>
    {
        public EmpresaValidation()
        {
            RuleFor(e => e.UsuarioProprietarioId)
                .NotEqual(Guid.Empty);

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
