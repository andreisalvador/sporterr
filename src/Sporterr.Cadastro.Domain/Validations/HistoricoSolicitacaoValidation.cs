using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class HistoricoSolicitacaoValidation : AbstractValidator<HistoricoSolicitacao>
    {
        public HistoricoSolicitacaoValidation()
        {
            RuleFor(h => h.SolicitacaoId)
                .NotEqual(Guid.Empty).WithMessage("A solicitação é obrigatória.");

            RuleFor(h => h.StatusSolicitacao)
                .IsInEnum().WithMessage("O status informado é inválido.");
        }
    }
}
