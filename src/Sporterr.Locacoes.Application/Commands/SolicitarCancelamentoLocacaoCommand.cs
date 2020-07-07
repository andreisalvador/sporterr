using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Locacoes.Application.Commands
{
    public class SolicitarCancelamentoLocacaoCommand : Command<SolicitarCancelamentoLocacaoCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public SolicitarCancelamentoLocacaoCommand(Guid solicitacaoId, Guid locacaoId)
            : base(new SolicitarCancelamentoLocacaoValidation())
        {
            LocacaoId = locacaoId;
            SolicitacaoId = solicitacaoId;         
        }


        private class SolicitarCancelamentoLocacaoValidation : AbstractValidator<SolicitarCancelamentoLocacaoCommand>
        {
            public SolicitarCancelamentoLocacaoValidation()
            {
                RuleFor(s => s.LocacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A locação precisa ser informada.");

                RuleFor(s => s.SolicitacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");
            }
        }
    }
}
