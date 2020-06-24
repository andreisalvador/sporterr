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
        {
            LocacaoId = locacaoId;
            Motivo = motivo;
        }

        protected override AbstractValidator<SolicitarCancelamentoLocacaoCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
