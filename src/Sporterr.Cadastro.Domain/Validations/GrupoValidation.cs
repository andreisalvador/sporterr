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

            RuleFor(g => g.NumeroMaximoMembros)
                .GreaterThanOrEqualTo((byte)2).WithMessage("O número maximo de participantes do grupo precisa ser maior que 1.");
        }
    }
}
