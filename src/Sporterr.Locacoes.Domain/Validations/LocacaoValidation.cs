using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sporterr.Locacoes.Domain.Validations
{
    public class LocacaoValidation : AbstractValidator<Locacao>
    {
        public LocacaoValidation()
        {
            RuleFor(l => l.EmpresaId)
                .NotEqual(Guid.Empty).WithMessage("A empresa é obrigatória.");

            RuleFor(l => l.SolicitacaoId)
                .NotEqual(Guid.Empty).WithMessage("A solicitação é obrigatória.");

            RuleFor(l => l.QuadraId)
                .NotEqual(Guid.Empty).WithMessage("A quadra é obrigatória.");

            RuleFor(l => l.UsuarioLocatarioId)
                .NotEqual(Guid.Empty).WithMessage("O usuário locatário é obrigatória.");

            RuleFor(l => l.TempoLocado)
                .NotEqual(TimeSpan.MinValue).WithMessage("O tempo locado é obrigatória.");
        }
    }
}
