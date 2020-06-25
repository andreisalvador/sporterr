using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void AdicionarEmpresa(Empresa usuario)
        {
            throw new NotImplementedException();
        }

        public void AdicionarGrupo(Grupo usuario)
        {
            throw new NotImplementedException();
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarEmpresa(Empresa usuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarGrupo(Grupo usuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Commit()
        {
            throw new NotImplementedException();
        }

        public Task<Empresa> ObterEmpresaPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Grupo> ObterGrupoPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
