using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using System;

namespace Sporterr.Cadastro.Domain
{
    public class Membro : Entity<Membro>
    {
        public Guid UsuarioId { get; private set; }
        public Guid GrupoId { get; private set; }

        //Ef rel.
        public Grupo Grupo { get; set; }
        public Usuario Usuario { get; set; }
        public Membro(Guid usuarioId)
        {
            UsuarioId = usuarioId;            
        }

        internal void AssociarGrupo(Guid grupoId) => GrupoId = grupoId;

        protected override AbstractValidator<Membro> ObterValidador() => new MembroValidation();
    }
}
