using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Application.Commands
{
    public class AdicionarPerfilHabilidadesCommand : Command<AdicionarPerfilHabilidadesCommand>
    {
        public Guid UsuarioId { get; private set; }
        public AdicionarPerfilHabilidadesCommand(Guid usuarioId) : base(new AdicionarPerfilHabilidadesValidation())
        {
            UsuarioId = usuarioId;
        }

        private class AdicionarPerfilHabilidadesValidation : AbstractValidator<AdicionarPerfilHabilidadesCommand>
        {
            public AdicionarPerfilHabilidadesValidation()
            {
                RuleFor(p => p.UsuarioId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O usuário é obrigatório.");
            }
        }
    }
}
