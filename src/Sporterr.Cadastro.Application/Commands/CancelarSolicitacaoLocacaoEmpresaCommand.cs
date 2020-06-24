using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class CancelarSolicitacaoLocacaoEmpresaCommand : Command<CancelarSolicitacaoLocacaoEmpresaCommand>
    {
        public Guid EmpresaId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public string MotivoCancelamento { get; private set; }

        public CancelarSolicitacaoLocacaoEmpresaCommand(Guid empresaId, Guid locacaoId, string motivoCancelamento)
        {
            EmpresaId = empresaId;
            LocacaoId = locacaoId;
            MotivoCancelamento = motivoCancelamento;
        }

        protected override AbstractValidator<CancelarSolicitacaoLocacaoEmpresaCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
