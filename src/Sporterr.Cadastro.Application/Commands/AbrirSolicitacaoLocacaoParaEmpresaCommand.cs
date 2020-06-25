using FluentValidation;
using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AbrirSolicitacaoLocacaoParaEmpresaCommand : Command<AbrirSolicitacaoLocacaoParaEmpresaCommand>
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public AbrirSolicitacaoLocacaoParaEmpresaCommand(Guid locacaoId, Guid empresaId, Guid quadraId, StatusSolicitacao status = StatusSolicitacao.AguardandoAprovacao)
            : base(new AbrirSolicitacaoLocacaoParaEmpresaValidation())
        {
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = status;
        }       

        private class AbrirSolicitacaoLocacaoParaEmpresaValidation : AbstractValidator<AbrirSolicitacaoLocacaoParaEmpresaCommand>
        {
            public AbrirSolicitacaoLocacaoParaEmpresaValidation()
            {
                RuleFor(s => s.LocacaoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("A locação é obrigatória");

                RuleFor(s => s.EmpresaId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("A empresa é obrigatória");

                RuleFor(s => s.QuadraId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("A quadra é obrigatória");
            }
        }
    }
}
