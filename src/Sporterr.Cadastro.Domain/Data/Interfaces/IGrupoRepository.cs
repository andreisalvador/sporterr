using Sporterr.Core.Data;
using System;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Domain.Data.Interfaces
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        void AdicionarGrupo(Grupo grupo);
        void AtualizarGrupo(Grupo grupo);
        Task<Grupo> ObterGrupoPorId(Guid grupoId);
        void AdicionarMembro(Membro membro);
        void ExcluirMembro(Membro membro);
        Task<Membro> ObterMembroPorId(Guid membroId);
    }
}
