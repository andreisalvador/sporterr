using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class RecusarSolicitacaoLocacaoCommand : Command<RecusarSolicitacaoLocacaoCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public RecusarSolicitacaoLocacaoCommand(Guid solicitacaoId, Guid empresaId)
        {
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
        }
        protected override AbstractValidator<RecusarSolicitacaoLocacaoCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
