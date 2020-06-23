using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AprovarSolicitacaoLocacaoCommand : Command<AprovarSolicitacaoLocacaoCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public AprovarSolicitacaoLocacaoCommand(Guid solicitacaoId, Guid empresaId)
        {
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
        }

        protected override AbstractValidator<AprovarSolicitacaoLocacaoCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
