using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class CancelarSolicitacaoLocacaoEmpresaCommand : Command<CancelarSolicitacaoLocacaoEmpresaCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string MotivoCancelamento { get; private set; }

        public CancelarSolicitacaoLocacaoEmpresaCommand(Guid solicitacaoId, Guid empresaId, string motivoCancelamento)
            :base(new CancelarSolicitacaoLocacaoEmpresaValidation())
        {
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            MotivoCancelamento = motivoCancelamento;
        }
        private class CancelarSolicitacaoLocacaoEmpresaValidation : AbstractValidator<CancelarSolicitacaoLocacaoEmpresaCommand>
        {
            public CancelarSolicitacaoLocacaoEmpresaValidation()
            {
                RuleFor(a => a.SolicitacaoId)
                    .NotEqual(Guid.Empty).WithMessage("A solicitação precisa ser informada.");

                RuleFor(a => a.EmpresaId)
                    .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");

                RuleFor(a => a.MotivoCancelamento)
                    .NotEmpty().WithMessage("O motivo do cancelamento precisa ser informado.");
            }
        }
    }
}
