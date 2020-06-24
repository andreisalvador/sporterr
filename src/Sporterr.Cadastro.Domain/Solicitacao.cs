using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sporterr.Cadastro.Domain
{
    public class Solicitacao : Entity<Solicitacao>
    {
        private readonly List<HistoricoSolicitacao> _historicos;

        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public IReadOnlyCollection<HistoricoSolicitacao> Historicos => _historicos.AsReadOnly();
        //Ef rel.
        public Empresa Empresa { get; set; }
        public Solicitacao(Guid locacaoId, Guid empresaId, Guid quadraId)
        {
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = StatusSolicitacao.AguardandoAprovacao;
            _historicos = new List<HistoricoSolicitacao>() { new HistoricoSolicitacao(Id, Status) };
        }

        public void Aprovar()
        {
            Status = StatusSolicitacao.Aprovada;
            IncluirHistoricoSolicitacao();
        }
        public void Cancelar(string motivoCancelamento)
        {
            Status = StatusSolicitacao.Cancelada;
            IncluirHistoricoSolicitacao(motivoCancelamento);
        }
        public void Recusar(string motivoRecusa)
        {
            Status = StatusSolicitacao.Recusada;
            IncluirHistoricoSolicitacao(motivoRecusa);
        }

        public void AguardarCancelamento(string motivoCancelamento)
        {
            Status = StatusSolicitacao.AguardandoCancelamento;
            IncluirHistoricoSolicitacao(motivoCancelamento);
        }

        private void IncluirHistoricoSolicitacao(string? descricao = null) =>
            _historicos.Add(new HistoricoSolicitacao(Id, Status, descricao));


        protected override AbstractValidator<Solicitacao> ObterValidador()
        {
            throw new NotImplementedException();
        }
    }
}
