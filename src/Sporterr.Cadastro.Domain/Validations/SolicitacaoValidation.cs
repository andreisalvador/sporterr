using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class SolicitacaoValidation : AbstractValidator<Solicitacao>
    {
        public SolicitacaoValidation()
        {
            RuleFor(s => s.LocacaoId)
                .NotEqual(Guid.Empty).WithMessage("A locação é obrigatória.");

            RuleFor(s => s.QuadraId)
                .NotEqual(Guid.Empty).WithMessage("A quadra é obrigatória.");

            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("O status informado é inválido.");
        }
    }
}
