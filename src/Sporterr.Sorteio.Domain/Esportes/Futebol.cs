using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Domain.Esportes
{
    public class Futebol : Esporte
    {
        public Futebol() : base(nameof(Futebol), Core.Enums.TipoEsporte.Futebol)
        {
            AdicionarHabilidade(new Habilidade("Chute"));
            AdicionarHabilidade(new Habilidade("Defesa"));
            AdicionarHabilidade(new Habilidade("Força"));
            AdicionarHabilidade(new Habilidade("Velocidade"));
            AdicionarHabilidade(new Habilidade("Drible"));
        }
    }
}
