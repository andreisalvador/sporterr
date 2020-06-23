using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarSolicitacaoEmpresaCommand : Command<AdicionarSolicitacaoEmpresaCommand>
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public AdicionarSolicitacaoEmpresaCommand(Guid locacaoId, Guid empresaId, Guid quadraId)
        {
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }
        protected override AbstractValidator<AdicionarSolicitacaoEmpresaCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
