using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class SolicitarCancelamentoLocacaoCommand : Command<SolicitarCancelamentoLocacaoCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public SolicitarCancelamentoLocacaoCommand(Guid solicitacaoId, Guid locacaoId, Guid usuarioId)
        {
            SolicitacaoId = solicitacaoId;
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

                RuleFor(s => s.UsuarioId)
                    .NotEqual(Guid.Empty).WithMessage("O usuário precisa ser informado.");

                RuleFor(s => s.SolicitacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");
            }
        }
    }
}
