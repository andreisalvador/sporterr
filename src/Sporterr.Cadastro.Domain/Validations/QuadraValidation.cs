using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class QuadraValidation : AbstractValidator<Quadra>
    {
        public QuadraValidation()
        {
            RuleFor(q => q.TempoLocacao)
                .NotEqual(TimeSpan.MinValue).WithMessage("O tempo de locação da quadra é obrigatório.");

            RuleFor(q => q.ValorPorTempoLocado)
                .GreaterThan(0).WithMessage("O valor por tempo de locação da quadra precisa ser maior que zero.");

            RuleFor(q => q.TipoEsporteQuadra)
                .IsInEnum().WithMessage("Tipo de esporte informado é inválido.");
        }
    }
}
