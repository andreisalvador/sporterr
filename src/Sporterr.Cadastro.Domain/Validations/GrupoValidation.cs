using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class GrupoValidation : AbstractValidator<Grupo>
    {
        public GrupoValidation()
        {
            RuleFor(g => g.UsuarioCriadorId)
                .NotEqual(Guid.Empty);
        }
    }
}
