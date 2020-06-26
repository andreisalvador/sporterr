using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class RemoverMembroGrupoCommand : Command<RemoverMembroGrupoCommand>
    {
        public Guid MembroId { get; private set; }
        public Guid GrupoId { get; private set; }

        public RemoverMembroGrupoCommand(Guid membroId, Guid grupoId) : base(new RemoverMembroGrupoValidation())
        {
            MembroId = membroId;
            GrupoId = grupoId;
        }

        private class RemoverMembroGrupoValidation : AbstractValidator<RemoverMembroGrupoCommand>
        {
            public RemoverMembroGrupoValidation()
            {
                RuleFor(m => m.MembroId)
                    .NotEqual(Guid.Empty).WithMessage("O membro precisa ser informado.");

                RuleFor(m => m.GrupoId)
                    .NotEqual(Guid.Empty).WithMessage("O grupo precisa ser informado.");

            }
        }
    }
}
