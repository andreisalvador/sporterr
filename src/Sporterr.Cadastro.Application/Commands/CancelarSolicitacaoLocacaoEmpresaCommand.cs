using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class CancelarSolicitacaoLocacaoEmpresaCommand : Command<CancelarSolicitacaoLocacaoEmpresaCommand>
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public string MotivoCancelamento { get; private set; }

        public CancelarSolicitacaoLocacaoEmpresaCommand(Guid solicitacaoId, Guid empresaId, Guid locacaoId, string motivoCancelamento)
            :base(new CancelarSolicitacaoLocacaoEmpresaValidation())
        {
            SolicitacaoId = solicitacaoId;
            EmpresaId = empresaId;
            LocacaoId = locacaoId;
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

                RuleFor(a => a.LocacaoId)
                   .NotEqual(Guid.Empty).WithMessage("A locação precisa ser informada.");

                RuleFor(a => a.MotivoCancelamento)
                    .NotEmpty().WithMessage("O motivo do cancelamento precisa ser informado.");
            }
        }
    }
}
