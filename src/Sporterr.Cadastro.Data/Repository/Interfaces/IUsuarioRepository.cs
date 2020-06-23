using Sporterr.Cadastro.Domain;
using Sporterr.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void AdicionarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(Guid id);

        void AdicionarEmpresa(Empresa usuario);
        void AtualizarEmpresa(Empresa usuario);
        Task<Empresa> ObterEmpresaPorId(Guid id);     

        void AdicionarGrupo(Grupo usuario);
        void AtualizarGrupo(Grupo usuario);
        Task<Grupo> ObterGrupoPorId(Guid id);

    }
}
