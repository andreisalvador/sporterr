using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AprovarSolicitacaoEmpresaCommand : Command<AprovarSolicitacaoEmpresaCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public AprovarSolicitacaoEmpresaCommand(Guid solicitacaoId, Guid empresaId)
            : base(new AprovarSolicitacaoLocacaoValidation())
        {
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
        }
        private class AprovarSolicitacaoLocacaoValidation : AbstractValidator<AprovarSolicitacaoEmpresaCommand>
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
