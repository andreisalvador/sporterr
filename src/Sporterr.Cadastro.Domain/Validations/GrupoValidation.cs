using FluentValidation;
using System;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class GrupoValidation : AbstractValidator<Grupo>
    {
        public GrupoValidation()
        {
            RuleFor(g => g.NomeGrupo)
                .NotEmpty().WithMessage("Nome do grupo precisa ser informado");
        }
    }
}
