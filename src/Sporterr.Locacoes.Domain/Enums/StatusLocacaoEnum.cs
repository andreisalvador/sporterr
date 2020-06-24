using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain.Enums
{
    public enum StatusLocacao
    {
        EmAberto = 1,
        AguardandoAprovacao,
        Recusada,
        Aprovada,
        AguardandoCancelamento,
        Cancelada
    }
}
