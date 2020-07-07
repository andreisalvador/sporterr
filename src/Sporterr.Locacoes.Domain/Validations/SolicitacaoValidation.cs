using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain.Validations
{
    public class SolicitacaoValidation : AbstractValidator<Solicitacao>
    {
        public SolicitacaoValidation()
        {
            RuleFor(s => s.EmpresaId).
                NotEqual(Guid.Empty).WithMessage("A empresa é obrigatória.");

            RuleFor(s => s.QuadraId)
                .NotEqual(Guid.Empty).WithMessage("A quadra é obrigatória.");

            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("O status informado é inválido.");
        }
    }
}
