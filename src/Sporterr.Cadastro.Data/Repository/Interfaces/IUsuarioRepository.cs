using Sporterr.Cadastro.Domain;
using Sporterr.Core.Data;
using System;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void AdicionarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(Guid usuarioId);

        void AdicionarEmpresa(Empresa empresa);
        void AtualizarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresaPorId(Guid empresaId);     

        void AdicionarGrupo(Grupo grupo);
        void AtualizarGrupo(Grupo grupo);
        Task<Grupo> ObterGrupoPorId(Guid grupoId);

    }
}
