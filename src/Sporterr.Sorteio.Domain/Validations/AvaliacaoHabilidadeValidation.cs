using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sporterr.Sorteio.Domain.Validations
{
    public class AvaliacaoHabilidadeValidation : AbstractValidator<AvaliacaoHabilidade>
    {
        public AvaliacaoHabilidadeValidation()
        {
            RuleFor(a => a.UsuarioAvaliadoId)
                .NotEqual(Guid.Empty).WithMessage("O usuário avaliádio é obrigatório.");
        }
    }
}
