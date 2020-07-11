using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class InativarEmpresaCommand : Command<InativarEmpresaCommand>
    {
        public Guid UsuarioProprietarioEmpresaId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public InativarEmpresaCommand(Guid usuarioProprietarioEmpresaId, Guid empresaId)
            : base(new InativarEmpresaUsuarioValidation())
        {
            UsuarioProprietarioEmpresaId = usuarioProprietarioEmpresaId;
            EmpresaId = empresaId;
        }

        private class InativarEmpresaUsuarioValidation : AbstractValidator<InativarEmpresaCommand>
        {
            public InativarEmpresaUsuarioValidation()
            {
                RuleFor(e => e.UsuarioProprietarioEmpresaId)
                    .NotEqual(Guid.Empty).WithMessage("O usuário proprietário da empresa precisa ser informado.");

                RuleFor(e => e.EmpresaId)
                    .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");
            }
        }
    }
}
