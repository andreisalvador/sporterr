using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class InativarQuadraEmpresaCommand : Command<InativarQuadraEmpresaCommand>
    {
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public InativarQuadraEmpresaCommand(Guid empresaId, Guid quadraId)
            : base(new InativarQuadraEmpresaValidation())
        {
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }

        private class InativarQuadraEmpresaValidation : AbstractValidator<InativarQuadraEmpresaCommand>
        {
            public InativarQuadraEmpresaValidation()
            {
                RuleFor(q => q.EmpresaId)
                    .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");

                RuleFor(q => q.QuadraId)
                    .NotEqual(Guid.Empty).WithMessage("A quadra precisa ser informada.");
            }
        }
    }
}
