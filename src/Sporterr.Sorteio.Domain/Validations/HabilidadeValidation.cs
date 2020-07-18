using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain.Validations
{
    public class HabilidadeValidation : AbstractValidator<Habilidade>
    {
        public HabilidadeValidation()
        {
            RuleFor(h => h.Nome)
                .NotEmpty().WithMessage("O nome da habilidade é obrigatório.");
        }
    }
}
