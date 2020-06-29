using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.Enums;
using System;

namespace Sporterr.Cadastro.Domain
{
    public class HistoricoSolicitacao : Entity<HistoricoSolicitacao>
    {
        public Guid SolicitacaoId { get; private set; }
        public StatusSolicitacao StatusSolicitacao { get; private set; }
        public string? Descricao { get; private set; }

        //Ef rel.
        public Solicitacao Solicitacao { get; set; }
        public HistoricoSolicitacao(Guid solicitacaoId, StatusSolicitacao statusSolicitacao, string descricao = null)
        {
            SolicitacaoId = solicitacaoId;
            StatusSolicitacao = statusSolicitacao;
            Descricao = descricao;
            Ativar();
            Validate();
        }

        public override void Validate() => Validate(this, new HistoricoSolicitacaoValidation());
    }
}
