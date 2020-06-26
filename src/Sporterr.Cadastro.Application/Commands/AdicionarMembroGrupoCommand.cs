using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarMembroGrupoCommand : Command<AdicionarMembroGrupoCommand>
    {
        public Guid UsuarioMembroId { get; private set; }
        public Guid GrupoId { get; private set; }

        public AdicionarMembroGrupoCommand(Guid usuarioMembroId, Guid grupoId) : base(new AdicionarMembroGrupoValidation())
        {
            UsuarioMembroId = usuarioMembroId;
            GrupoId = grupoId;
        }

        private class AdicionarMembroGrupoValidation  : AbstractValidator<AdicionarMembroGrupoCommand>
        {
            public AdicionarMembroGrupoValidation()
            {
                RuleFor(m => m.UsuarioMembroId)
                    .NotEqual(Guid.Empty).WithMessage("O novo membro precisa ser informado.");

                RuleFor(m => m.GrupoId)
                    .NotEqual(Guid.Empty).WithMessage("O grupo precisa ser informado.");
            }
        }
    }
}
