using FluentValidation;
using Sporterr.Cadastro.Domain.Validations;
using Sporterr.Core.DomainObjects;
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
            Validar();
        }


        public void AdicionarEmpresa(Empresa empresa)
        {
            if (empresa.Validar() && !EmpresaPertenceUsuario(empresa))
            {
                empresa.AssociarUsuarioProprietario(Id);
                _empresas.Add(empresa);
            }
        }
        public void InativarEmpresa(Empresa empresa)
        {
            if (EmpresaPertenceUsuario(empresa))
            {
                Empresa empresaExistente = _empresas.SingleOrDefault(e => e.Id.Equals(empresa.Id));

                if (empresaExistente.PossuiQuadras())
                {
                    foreach (Quadra quadra in empresaExistente.Quadras)
                        empresaExistente.InativarQuadra(quadra);

                    empresaExistente.Inativar();
                }
            }
        }

        public void AdicionarGrupo(Grupo grupo)
        {
            if (grupo.Validar() && !GrupoPertenceUsuario(grupo))
            {
                grupo.AssociarUsuarioCriador(Id);
                _grupos.Add(grupo);
            }
        }

        public void RemoverGrupo(Grupo grupo)
        {
            if (grupo.Validar() && GrupoPertenceUsuario(grupo)) _grupos.Remove(grupo);
        }

        public void RemoverMembroDoGrupo(Usuario membro, Grupo grupo)
        {
            if (GrupoPertenceUsuario(grupo) && NovoMembroFazParteDoGrupo(membro, grupo))
            {
                Grupo grupoExistente = _grupos.SingleOrDefault(g => g.Id == grupo.Id);
                grupoExistente.RemoverMembro(membro);
            }
        }

        public void AdicionarMembroAoGrupo(Usuario membro, Grupo grupo)
        {
            if (GrupoPertenceUsuario(grupo) && !NovoMembroFazParteDoGrupo(membro, grupo))
            {
                Grupo grupoExistente = _grupos.SingleOrDefault(g => g.Id == grupo.Id);
                grupoExistente.AssociarMembro(membro);
            }
        }

        internal bool EmpresaPertenceUsuario(Empresa empresa) => _empresas.Any(e => e.Equals(empresa) && empresa.UsuarioProprietarioId.Equals(Id));
        internal bool GrupoPertenceUsuario(Grupo grupo) => _grupos.Any(g => g.Equals(grupo) && grupo.UsuarioCriadorId.Equals(Id));
        internal bool NovoMembroFazParteDoGrupo(Usuario usuario, Grupo grupo) => _grupos.Any(g => g.Id.Equals(grupo.Id) && grupo.Membros.Any(m => m.Id.Equals(usuario.Id)));

        protected override AbstractValidator<Usuario> ObterValidador() => new UsuarioValidation();


    }
}
