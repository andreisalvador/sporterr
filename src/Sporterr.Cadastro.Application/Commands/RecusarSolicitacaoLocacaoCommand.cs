using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class RecusarSolicitacaoLocacaoCommand : Command<RecusarSolicitacaoLocacaoCommand>
    {
        public Guid LocacaoId { get; private set; }
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string Motivo { get; private set; }
        public RecusarSolicitacaoLocacaoCommand(Guid locacaoId, Guid solicitacaoId, Guid empresaId, string motivo)
        {
            LocacaoId = locacaoId;
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            Motivo = motivo;
        }


        protected override AbstractValidator<RecusarSolicitacaoLocacaoCommand> GetValidator() => new RecusarSolicitacaoLocacaoValidation();

        private class RecusarSolicitacaoLocacaoValidation : AbstractValidator<RecusarSolicitacaoLocacaoCommand>
        {
            public RecusarSolicitacaoLocacaoValidation()
            {
                RuleFor(a => a.SolicitacaoId)
                   .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");

                RuleFor(a => a.EmpresaId)
                   .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");
            }
        }
    }
}
