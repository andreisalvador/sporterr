using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporterr.Cadastro.Domain
{
    public class Grupo : Entity<Grupo>, IAggregateRoot
    {
        private readonly List<Usuario> _membros;

        public Guid UsuarioCriadorId { get; private set; }
        public IReadOnlyCollection<Usuario> Membros => _membros.AsReadOnly();

        //Ef rel.
        public Usuario UsuarioCriador { get; set; }

        public Grupo(Guid usuarioCriadorId)
        {
            UsuarioCriadorId = usuarioCriadorId;
            _membros = new List<Usuario>();
            Validar();
        }

        internal void AssociarMembro(Usuario membro)
        {
            if (membro.Validar() && !_membros.Any(u => u.Equals(membro))) _membros.Add(membro); //except dps
        }

        internal void RemoverMembro(Usuario membro)
        {
            if (MembroPertenceGrupo(membro)) _membros.Remove(membro);
        }        

        public bool MembroPertenceGrupo(Usuario membro) => _membros.Any(u => u.Equals(membro));

        protected override AbstractValidator<Grupo> ObterValidador() => new GrupoValidation();
       
    }
}
