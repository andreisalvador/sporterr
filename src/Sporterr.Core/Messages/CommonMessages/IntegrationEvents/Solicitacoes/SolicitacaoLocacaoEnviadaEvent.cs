using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes
{
    public class SolicitacaoLocacaoEnviadaEvent : IntegrationEvent
    {
        public Guid SolicitacaoId { get; private set; }
        public Guid UsuarioLocatarioId { get; private set; }        
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }                

        public SolicitacaoLocacaoEnviadaEvent(Guid solicitacaoId, Guid usuarioLocatarioId, Guid empresaId, Guid quadraId, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao)
        {
            AggregateId = solicitacaoId;
            SolicitacaoId = solicitacaoId;
            UsuarioLocatarioId = usuarioLocatarioId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;                        
        }
    }
}