using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{ 
    public class SolicitacaoLocacaoEnviadaEvent : Event
    {
        public Guid UsuarioLocatarioId { get; private set; }
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }
        public decimal Valor { get; private set; }

        public SolicitacaoLocacaoEnviadaEvent(Guid usuarioLocatarioId, Guid locacaoId, Guid quadraId, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao, decimal valor)
        {
            AggregateId = locacaoId;
            UsuarioLocatarioId = usuarioLocatarioId;
            LocacaoId = locacaoId;
            QuadraId = quadraId;
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Valor = valor;
        }
    }
}