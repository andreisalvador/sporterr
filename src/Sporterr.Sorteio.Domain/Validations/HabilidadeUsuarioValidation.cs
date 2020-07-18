using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain.Validations
{
    public class HabilidadeUsuarioValidation : AbstractValidator<HabilidadeUsuario>
    {
        public HabilidadeUsuarioValidation()
        {
            RuleFor(h => h.HabilidadeId)
                .NotEqual(Guid.Empty).WithMessage("A habilidade é obrigatória.");

            RuleFor(h => h.EsporteId)
                .NotEqual(Guid.Empty).WithMessage("O esporte é obrigatório.");
        }
    }
}
