using Sporterr.Core.Data;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Data.Repository.Interfaces
{
    public interface IPerfilHabilidadesRepository : IRepository<PerfilHabilidades>
    {
        void AdicionarPerfilHabilidades(PerfilHabilidades perfilHabilidades);
        void AtualizarPerfilHabilidades(PerfilHabilidades perfilHabilidades);
    }
}
