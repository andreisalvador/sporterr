using FluentValidation;
using System;

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
