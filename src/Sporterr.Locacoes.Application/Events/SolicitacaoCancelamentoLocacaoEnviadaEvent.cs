using Sporterr.Core.Messages;
using System;

namespace Sporterr.Locacoes.Application.Events
{
    public class SolicitacaoCancelamentoLocacaoEnviadaEvent : Event
    {
        public Guid LocacaoCanceladaId { get; private set; }
        public Guid UsuarioLocatarioId { get; private set; }
        public Guid QuadraId { get; private set; }

        public SolicitacaoCancelamentoLocacaoEnviadaEvent(Guid locacaoCanceladaId, Guid usuarioLocatarioId, Guid quadraId)
        {
            AggregateId = locacaoCanceladaId;
            LocacaoCanceladaId = locacaoCanceladaId;
            UsuarioLocatarioId = usuarioLocatarioId;
            QuadraId = quadraId;
        }
    }
}