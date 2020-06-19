using FluentValidation;
using Sporterr.Cadastro.Domain.Resources;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sporterr.Cadastro.Domain
{
    public class Usuario : Entity<Usuario>, IAggregateRoot
    {
        private readonly List<Empresa> _empresas;
        private readonly List<Grupo> _grupos;

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public IReadOnlyCollection<Empresa> Empresas => _empresas.AsReadOnly();
        public IReadOnlyCollection<Grupo> Grupos => _grupos.AsReadOnly();

        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            _empresas = new List<Empresa>();
            _grupos = new List<Grupo>();
            Validar();
        }


        internal void AdicionarEmpresa(Empresa empresa)
        {
            if (empresa.Validar() && !EmpresaPertenceUsuario(empresa)) _empresas.Add(empresa);
        }
        internal void RemoverEmpresa(Empresa empresa)
        {
            if (EmpresaPertenceUsuario(empresa)) _empresas.Remove(empresa);
        }

        internal void AdicionarQuadraNaEmpresa(Empresa empresa, Quadra quadra)
        {
            if (quadra.Validar() && EmpresaPertenceUsuario(empresa))
            {
                Empresa empresaExistente = _empresas.FirstOrDefault(e => e.Id.Equals(empresa.Id));
                empresaExistente.AdicionarQuadra(quadra);
            }
        }

        internal void RemoverQuadraDaEmpresa(Empresa empresa, Quadra quadra)
        {
            if (quadra.Validar() && EmpresaPertenceUsuario(empresa))
            {
                Empresa empresaExistente = _empresas.FirstOrDefault(e => e.Id.Equals(empresa.Id));
                empresaExistente.RemoverQuadra(quadra);
            }
        }

        internal void AdicionarGrupo(Grupo grupo)
        {
            if(grupo.Validar() && !GrupoPertenceUsuario(grupo))  _grupos.Add(grupo);
        }

        internal void RemoverGrupo(Grupo grupo)
        {
            if (grupo.Validar() && GrupoPertenceUsuario(grupo)) _grupos.Remove(grupo);
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

        protected override AbstractValidator<Usuario> ObterValidador() => new UsuarioValidation();

       
    }
}
