using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        private readonly List<Empresa> _empresas;
        private readonly List<Grupo> _grupos;

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public IReadOnlyCollection<Empresa> Empresas => _empresas.AsReadOnly();
        public IReadOnlyCollection<Grupo> Grupos => _grupos.AsReadOnly();

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }


        internal void AdicionarEmpresa(Empresa empresa)
        {
            if (!EmpresaPertenceUsuario(empresa)) _empresas.Add(empresa);
        }

        internal void RemoverQuadra(Empresa empresa)
        {
            if (EmpresaPertenceUsuario(empresa)) _empresas.Remove(empresa);
        }

        internal void AdicionarGrupo(Grupo grupo)
        {
            if(!GrupoPertenceUsuario(grupo))  _grupos.Add(grupo);
        }

        internal void RemoverGrupo(Grupo grupo)
        {
            if (GrupoPertenceUsuario(grupo)) _grupos.Remove(grupo);
        }

        internal void SairDoGrupo(Grupo grupo)
        {
            if (UsuarioFazParteGrupo(grupo))
            {
                Grupo grupoExistente = _grupos.FirstOrDefault(g => g.Id == grupo.Id);
                grupoExistente.RemoverMembro(this);

                if (!GrupoPertenceUsuario(grupoExistente)) RemoverGrupo(grupoExistente);
            }
        }

        public bool EmpresaPertenceUsuario(Empresa empresa) => _empresas.Any(e => e.Equals(empresa) && empresa.UsuarioProprietarioId.Equals(Id));
        public bool GrupoPertenceUsuario(Grupo grupo) => _grupos.Any(g => g.Equals(grupo) && grupo.UsuarioCriadorId.Equals(Id));
        public bool UsuarioFazParteGrupo(Grupo grupo) => _grupos.Any(g => g.Equals(grupo) && grupo.Membros.Any(m => m.Id.Equals(Id)));
    }
}
