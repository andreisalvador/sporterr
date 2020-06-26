using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain.Validations
{
    public class LocacaoValidation : AbstractValidator<Locacao>
    {
        public LocacaoValidation()
        {
            //RuleFor(q => q.EmpresaId).
        }
    }
}
