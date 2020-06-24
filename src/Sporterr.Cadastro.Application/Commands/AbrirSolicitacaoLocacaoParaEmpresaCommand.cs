using FluentValidation;
using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AbrirSolicitacaoLocacaoParaEmpresaCommand : Command<AbrirSolicitacaoLocacaoParaEmpresaCommand>
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public AbrirSolicitacaoLocacaoParaEmpresaCommand(Guid locacaoId, Guid empresaId, Guid quadraId, StatusSolicitacao status = StatusSolicitacao.AguardandoAprovacao)
        {
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = status;
        }
        protected override AbstractValidator<AbrirSolicitacaoLocacaoParaEmpresaCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
