using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Application.Events
{
    public class LocacaoCriadaEvent : Event
    {
        public Guid LocacaoId { get; private set; }
        public LocacaoCriadaEvent(Guid locacaoId)
        {
            AggregateId = locacaoId;
            LocacaoId = locacaoId;
        }
    }
}
