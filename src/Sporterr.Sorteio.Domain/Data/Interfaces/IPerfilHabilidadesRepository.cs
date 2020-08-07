using Sporterr.Core.Data;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Domain.Data.Interfaces
{
    public interface IPerfilHabilidadesRepository : IRepository<PerfilHabilidades>
    {
        void AdicionarPerfilHabilidades(PerfilHabilidades perfilHabilidades);
        void AtualizarPerfilHabilidades(PerfilHabilidades perfilHabilidades);
        Task<PerfilHabilidades> ObterPorId(Guid perfilId);
        Task<PerfilHabilidades> ObterPorIdComHabilidades(Guid perfilId);
        Task<bool> Existe(Guid perfilId);
        Task<bool> ExisteParaUsuario(Guid usuarioId);
    }
}
