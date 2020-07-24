using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Application.Commands
{
    public class VincularEsportePerfilHabilidadesCommand : Command<VincularEsportePerfilHabilidadesCommand>
    {
        public Guid PerfilHabilidadesId { get; private set; }
        public Guid EsporteId { get; private set; }

        public VincularEsportePerfilHabilidadesCommand(Guid perfilHabilidadesId, Guid esporteId) : base(new VincularEsportePerfilHabilidadeValidation())
        {
            PerfilHabilidadesId = perfilHabilidadesId;
            EsporteId = esporteId;
        }       

        private class VincularEsportePerfilHabilidadeValidation : AbstractValidator<VincularEsportePerfilHabilidadesCommand>
        {
            public VincularEsportePerfilHabilidadeValidation()
            {
                RuleFor(v => v.PerfilHabilidadesId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O perfil precisa ser informado.");

                RuleFor(v => v.EsporteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O esporte precisa ser informado.");
            }
        }
    }
}
