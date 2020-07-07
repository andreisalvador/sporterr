using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AprovarSolicitacaoLocacaoCommand : Command<AprovarSolicitacaoLocacaoCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public AprovarSolicitacaoLocacaoCommand(Guid solicitacaoId, Guid empresaId, Guid quadraId)
            : base(new AprovarSolicitacaoLocacaoValidation())
        {
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }
        private class AprovarSolicitacaoLocacaoValidation : AbstractValidator<AprovarSolicitacaoLocacaoCommand>
        {
            public AprovarSolicitacaoLocacaoValidation()
            {
                RuleFor(a => a.SolicitacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");

                RuleFor(a => a.EmpresaId)
                   .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");

                RuleFor(a => a.QuadraId)
                  .NotEqual(Guid.Empty).WithMessage("A quadra precisa ser informada.");
            }
        }
    }
}
