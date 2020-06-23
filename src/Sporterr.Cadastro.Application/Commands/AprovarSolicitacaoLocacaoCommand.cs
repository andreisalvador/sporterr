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

        protected override AbstractValidator<AprovarSolicitacaoLocacaoCommand> GetValidator() => new AprovarSolicitacaoLocacaoValidation();

        private class AprovarSolicitacaoLocacaoValidation : AbstractValidator<AprovarSolicitacaoLocacaoCommand>
        {
            public AprovarSolicitacaoLocacaoValidation()
            {
                RuleFor(a => a.SolicitacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");

                RuleFor(a => a.EmpresaId)
                   .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");
            }
        }
    }
}
