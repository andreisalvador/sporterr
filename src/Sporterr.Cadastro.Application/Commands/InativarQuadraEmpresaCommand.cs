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
        {
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }

        protected override AbstractValidator<InativarQuadraEmpresaCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
