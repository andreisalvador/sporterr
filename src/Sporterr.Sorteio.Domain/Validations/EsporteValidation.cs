using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain.Validations
{
    public class EsporteValidation : AbstractValidator<Esporte>
    {
        public EsporteValidation()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome do esporte é obrigatório.");

            RuleFor(e => e.TipoEsporte)
                .IsInEnum().WithMessage("O tipo de esporte informado não é válido.");
        }
    }
}
