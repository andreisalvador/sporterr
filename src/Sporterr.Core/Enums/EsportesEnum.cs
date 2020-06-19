using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Enums
{
    [Flags]
    public enum Esporte
    {
        Futebol = 1,
        Volei = 2,
        Basquete = 4,
        Handebol = 8,
        Tenis = 16
    }
}
