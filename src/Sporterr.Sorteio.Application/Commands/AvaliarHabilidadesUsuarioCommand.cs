using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporterr.Sorteio.Application.Commands
{
    public class AvaliarHabilidadesUsuarioCommand : Command<AvaliarHabilidadesUsuarioCommand>
    {
        public Guid PerfilHabilidadesId { get; private set; }
        public IDictionary<Guid, double> HabilidadesAvaliadas { get; private set; }
        public AvaliarHabilidadesUsuarioCommand(Guid perfilHabilidadesId, IDictionary<Guid, double> habilidadesAvaliadas) : base(new AvaliarHabilidadeUsuarioValidation())
        {
            PerfilHabilidadesId = perfilHabilidadesId;
            HabilidadesAvaliadas = habilidadesAvaliadas ?? new Dictionary<Guid, double>();
        }

        private class AvaliarHabilidadeUsuarioValidation : AbstractValidator<AvaliarHabilidadesUsuarioCommand>
        {
            public AvaliarHabilidadeUsuarioValidation()
            {
                RuleFor(a => a.PerfilHabilidadesId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O perfil precisa ser informado.");

                RuleFor(a => a.HabilidadesAvaliadas)
                    .NotNull()                    
                    .WithMessage("As avaliações precisam ser especificadas.");
            }
        }
    }
}
