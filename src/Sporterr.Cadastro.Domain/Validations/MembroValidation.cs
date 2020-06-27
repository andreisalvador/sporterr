using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class MembroValidation : AbstractValidator<Membro>
    {
        public MembroValidation()
        {
            RuleFor(m => m.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O usuário é obrigatório.");
        }
    }
}
