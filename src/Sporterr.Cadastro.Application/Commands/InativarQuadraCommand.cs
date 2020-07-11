using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class InativarQuadraCommand : Command<InativarQuadraCommand>
    {
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public InativarQuadraCommand(Guid empresaId, Guid quadraId)
            : base(new InativarQuadraEmpresaValidation())
        {
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }

        private class InativarQuadraEmpresaValidation : AbstractValidator<InativarQuadraCommand>
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
