using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class RecusarSolicitacaoLocacaoCommand : Command<RecusarSolicitacaoLocacaoCommand>
    {   
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string Motivo { get; private set; }
        public RecusarSolicitacaoLocacaoCommand(Guid solicitacaoId, string motivo)
            : base(new RecusarSolicitacaoLocacaoValidation())
        {            
            SolicitacaoId = solicitacaoId;            
            Motivo = motivo;
        }
        private class RecusarSolicitacaoLocacaoValidation : AbstractValidator<RecusarSolicitacaoLocacaoCommand>
        {
            public RecusarSolicitacaoLocacaoValidation()
            {
                RuleFor(a => a.SolicitacaoId)
                   .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");
            }
        }
    }
}
