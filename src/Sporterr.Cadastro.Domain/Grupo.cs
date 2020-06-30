using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Grupo : Entity<Grupo>, IActivationEntity, IAggregateRoot
    {
        private readonly List<Membro> _membros;

        public Guid UsuarioCriadorId { get; private set; }
        public string NomeGrupo { get; private set; }
        public sbyte NumeroMaximoMembros { get; private set; }
        public sbyte QuantidadeMembros { get; private set; }
        public IReadOnlyCollection<Membro> Membros => _membros.AsReadOnly();
        public bool Ativo { get; private set; }

        //Ef rel.
        public Usuario UsuarioCriador { get; set; }


        public Grupo(string nomeGrupo, sbyte numeroMaximoMembros = 5)
        {
            NomeGrupo = nomeGrupo;
            NumeroMaximoMembros = numeroMaximoMembros;
            _membros = new List<Membro>();
            QuantidadeMembros = 0;
            Ativar();
            Validate();
        }

        internal void AssociarUsuarioCriador(Guid usuarioCriadorId) => UsuarioCriadorId = usuarioCriadorId;

        public void AdicionarMembro(Membro membro)
        {
            membro.Validate();

            if (GrupoEstaCheio())
                throw new DomainException($"Não é possível adicionar um novo membro ao grupo '{NomeGrupo}' pois o número máximo ({NumeroMaximoMembros}) de participantes do grupo foi atingido.");

            if (MembroPertenceGrupo(membro)) throw new DomainException($"Não é possível adicionar o novo membro pois ele já faz parte do grupo '{NomeGrupo}'.");

            membro.AssociarGrupo(Id);
            _membros.Add(membro);
            QuantidadeMembros++;
        }

        public void RemoverMembro(Membro membro)
        {
            membro.Validate();

            if (!MembroPertenceGrupo(membro)) throw new DomainException($"Não é possível remover o membro pois ele não faz parte do grupo '{NomeGrupo}'.");

            _membros.Remove(membro);
            QuantidadeMembros--;
        }

        public bool MembroPertenceGrupo(Membro membro) => _membros.Any(u => u.Equals(membro));

        public bool GrupoEstaCheio() => QuantidadeMembros == NumeroMaximoMembros;

        public override void Validate() => Validate(this, new GrupoValidation());

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
