﻿using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Enums;
using Sporterr.Locacoes.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain
{
    public class Solicitacao : Entity<Solicitacao>, IAggregateRoot
    {
        private readonly List<HistoricoSolicitacao> _historicos;

        public Guid UsuarioLocatarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }
        public TimeSpan TempoTotalLocacaoSolicitado { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public IReadOnlyCollection<HistoricoSolicitacao> Historicos => _historicos.AsReadOnly();        
        
        public Solicitacao(Guid usuarioLocatarioId, Guid empresaId, Guid quadraId, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao)
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            QuadraId = quadraId;
            EmpresaId = empresaId;
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            TempoTotalLocacaoSolicitado = dataHoraFimLocacao - dataHoraInicioLocacao;
            Status = StatusSolicitacao.AguardandoAprovacao;
            _historicos = new List<HistoricoSolicitacao>() { new HistoricoSolicitacao(Id, Status) };            
            Validate();
        }

        public void Aprovar()
        {
            if (Status != StatusSolicitacao.AguardandoAprovacao) throw new DomainException("Só é possível aprovar solicitações que estavam aguardando aprovação.");

            Status = StatusSolicitacao.Aprovada;
            IncluirHistoricoSolicitacao();
        }
        public void Cancelar(string motivoCancelamento)
        {
            if (string.IsNullOrWhiteSpace(motivoCancelamento)) throw new DomainException("Para cancelar uma solicição, você precisa informar o motivo de cancelamento.");

            if (Status != StatusSolicitacao.Aprovada) throw new DomainException("Só é possível cancelar solicitações que estavam aprovadas.");

            Status = StatusSolicitacao.Cancelada;
            IncluirHistoricoSolicitacao(motivoCancelamento);
        }
        public void Recusar(string motivoRecusa)
        {
            if (string.IsNullOrWhiteSpace(motivoRecusa)) throw new DomainException("Para recusar uma solicição, você precisa informar o motivo da recusa.");

            if (Status != StatusSolicitacao.AguardandoAprovacao) throw new DomainException("Só é possível recusar solicitações que estavam aguardando aprovação.");

            Status = StatusSolicitacao.Recusada;
            IncluirHistoricoSolicitacao(motivoRecusa);
        }

        public void AguardarCancelamento()
        {
            if (Status != StatusSolicitacao.Aprovada) throw new DomainException("Só é possível solicitar cancelamento de solicitações que estavam aprovadas.");

            Status = StatusSolicitacao.AguardandoCancelamento;
            IncluirHistoricoSolicitacao();
        }

        private void IncluirHistoricoSolicitacao(string? descricao = null) =>
            _historicos.Add(new HistoricoSolicitacao(Id, Status, descricao));

        public bool EstaPendente() => Status == StatusSolicitacao.AguardandoAprovacao || Status == StatusSolicitacao.AguardandoCancelamento;

        public override void Validate() => Validate(this, new SolicitacaoValidation());
    }
}
