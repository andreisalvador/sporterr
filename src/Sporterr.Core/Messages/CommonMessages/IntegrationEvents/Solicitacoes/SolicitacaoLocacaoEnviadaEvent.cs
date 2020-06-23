using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{ 
    public class SolicitacaoLocacaoEnviadaEvent : Event
    {
        public Guid LocacaoId;
        public Guid QuadraId;
        public DateTime DataHoraInicioLocacao;
        public DateTime DataHoraFimLocacao;
        public decimal Valor;

        public SolicitacaoLocacaoEnviadaEvent(Guid locacaoId, Guid quadraId, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao, decimal valor)
        {
            AggregateId = locacaoId;
            LocacaoId = locacaoId;
            QuadraId = quadraId;
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Valor = valor;
        }
    }
}