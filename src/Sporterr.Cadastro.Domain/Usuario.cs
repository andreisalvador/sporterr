using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using System.Collections.Generic;
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
            Ativar();
            Validate();
        }

        public void AdicionarEmpresa(Empresa empresa)
        {
            empresa.Validate();

            if (EmpresaPertenceUsuario(empresa))
                throw new DomainException($"A empresa informada ja pertence ao usuário '{Nome}'.");

            empresa.AssociarUsuarioProprietario(Id);
            _empresas.Add(empresa);
        }
        public void InativarEmpresa(Empresa empresa)
        {
            empresa.Validate();

            if (!EmpresaPertenceUsuario(empresa))
                throw new DomainException($"A empresa informada não pertence ao usuário '{Nome}'.");

            Empresa empresaExistente = _empresas.SingleOrDefault(e => e.Id.Equals(empresa.Id));

            if (empresaExistente.PossuiQuadras())
            {
                foreach (Quadra quadra in empresaExistente.Quadras)
                    empresaExistente.InativarQuadra(quadra);
            }

            empresaExistente.Inativar();
        }

        public void AdicionarGrupo(Grupo grupo)
        {
            grupo.Validate();

            if (GrupoPertenceUsuario(grupo))
                throw new DomainException($"O grupo informado já pertence ao usuário '{Nome}'.");

            grupo.AssociarUsuarioCriador(Id);
            _grupos.Add(grupo);
        }

        public void RemoverGrupo(Grupo grupo)
        {
            grupo.Validate();

            if (!GrupoPertenceUsuario(grupo))
                throw new DomainException($"O grupo informado não pertence ao usuário '{Nome}'.");

            _grupos.Remove(grupo);
        }

        internal bool EmpresaPertenceUsuario(Empresa empresa) => _empresas.Any(e => e.Equals(empresa) && empresa.UsuarioProprietarioId.Equals(Id));
        internal bool GrupoPertenceUsuario(Grupo grupo) => _grupos.Any(g => g.Equals(grupo) && grupo.UsuarioCriadorId.Equals(Id));
        internal bool NovoMembroFazParteDoGrupo(Usuario usuario, Grupo grupo) => _grupos.Any(g => g.Id.Equals(grupo.Id) && grupo.Membros.Any(m => m.Id.Equals(usuario.Id)));

        public override void Validate() => Validate(this, new UsuarioValidation());
    }
}
