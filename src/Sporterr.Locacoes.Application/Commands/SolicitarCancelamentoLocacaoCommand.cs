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
        public Guid UsuarioId { get; private set; }
        public SolicitarCancelamentoLocacaoCommand(Guid locacaoId, Guid usuarioId)
        {
            LocacaoId = locacaoId;
            UsuarioId = usuarioId;
        }

        protected override AbstractValidator<SolicitarCancelamentoLocacaoCommand> GetValidator() => new SolicitarCancelamentoLocacaoValidation();

        private class SolicitarCancelamentoLocacaoValidation : AbstractValidator<SolicitarCancelamentoLocacaoCommand>
        {
            public SolicitarCancelamentoLocacaoValidation()
            {
                RuleFor(s => s.LocacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A locação precisa ser informada.");
            }
        }
    }
}
