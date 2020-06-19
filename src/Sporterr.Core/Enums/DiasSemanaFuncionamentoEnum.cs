using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Enums
{
    [Flags]
    public enum DiasSemanaFuncionamento
    {
        Segunda = 1,
        Terca = 2,
        Quarta = 4,
        Quinta = 8, 
        Sexta = 16,
        Sabado = 32,
        Domingo = 64,
        DiasUteis = Segunda + Terca + Quarta + Quinta + Sexta,
        FinalDeSemana = Sabado + Domingo,
        Todos = DiasUteis + FinalDeSemana
    }
}
