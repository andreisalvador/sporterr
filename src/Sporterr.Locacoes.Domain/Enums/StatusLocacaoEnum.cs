using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Domain.Enums
{
    public enum StatusLocacao
    {
        AguardandoAprovacao = 1,
        Recusada,
        Aprovada,
        AguardandoCancelamento,
        Cancelada
    }
}
