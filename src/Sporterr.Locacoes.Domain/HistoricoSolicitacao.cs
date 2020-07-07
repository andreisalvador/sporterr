using Sporterr.Core.DomainObjects;
using Sporterr.Core.Enums;
using Sporterr.Locacoes.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain
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
            Validate();
        }

        public override void Validate() => Validate(this, new HistoricoSolicitacaoValidation());
    }
}
