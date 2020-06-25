using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Application.Commands
{
    public class SolicitarCancelamentoLocacaoCommand : Command<SolicitarCancelamentoLocacaoCommand>
    {
        public Guid LocacaoId { get; private set; }
        public string Motivo { get; private set; }
        public SolicitarCancelamentoLocacaoCommand(Guid locacaoId, string motivo)
            : base(new SolicitarCancelamentoLocacaoValidation())
        {
            LocacaoId = locacaoId;
            Motivo = motivo;
        }


        private class SolicitarCancelamentoLocacaoValidation : AbstractValidator<SolicitarCancelamentoLocacaoCommand>
        {
            public SolicitarCancelamentoLocacaoValidation()
            {
                RuleFor(s => s.LocacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A locação precisa ser informada.");

                RuleFor(s => s.Motivo)
                    .NotEmpty().WithMessage("O motivo do cancelamento precisa ser informado.");
            }
        }
    }
}
