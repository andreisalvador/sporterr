using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Grupo : Entity<Grupo>, IAggregateRoot
    {
        private readonly List<Membro> _membros;

        public Guid UsuarioCriadorId { get; private set; }
        public string NomeGrupo { get; private set; }
        public sbyte NumeroMaximoMembros { get; private set; }
        public sbyte QuantidadeMembros { get; private set; }
        public IReadOnlyCollection<Membro> Membros => _membros.AsReadOnly();

        //Ef rel.
        public Usuario UsuarioCriador { get; set; }

        public Grupo(string nomeGrupo, sbyte numeroMaximoMembros = 5)
        {            
            NomeGrupo = nomeGrupo;
            NumeroMaximoMembros = numeroMaximoMembros;
            _membros = new List<Membro>();
            QuantidadeMembros = 0;
            Ativar();
            Validar();
        }

        internal void AssociarUsuarioCriador(Guid usuarioCriadorId) => UsuarioCriadorId = usuarioCriadorId;

        public void AdicionarMembro(Membro membro)
        {
            if (!GrupoEstaCheio() && !MembroPertenceGrupo(membro) && membro.Validar())
            {
                membro.AssociarGrupo(Id);
                _membros.Add(membro); //except dps
                QuantidadeMembros++;
            }
        }

        public void RemoverMembro(Membro membro)
        {
            if (MembroPertenceGrupo(membro))
            {
                _membros.Remove(membro);
                QuantidadeMembros--;
            }
        }        

        public bool MembroPertenceGrupo(Membro membro) => _membros.Any(u => u.Equals(membro));

        public bool GrupoEstaCheio() => QuantidadeMembros == NumeroMaximoMembros;
        protected override AbstractValidator<Grupo> ObterValidador() => new GrupoValidation();
       
    }
}
