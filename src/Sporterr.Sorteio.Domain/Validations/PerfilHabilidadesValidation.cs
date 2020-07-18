using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain.Validations
{
    public class PerfilHabilidadesValidation : AbstractValidator<PerfilHabilidades>
    {
        public PerfilHabilidadesValidation()
        {
            RuleFor(p => p.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O usuário é obrigatório.");
        }
    }
}
