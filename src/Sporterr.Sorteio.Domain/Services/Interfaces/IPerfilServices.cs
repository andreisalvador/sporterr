using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Domain.Services.Interfaces
{
    public interface IPerfilServices
    {
        Task AdicionarNovoEsporte(Guid perfilId, Guid esporteId);
        Task AvaliarPerfil(Guid perfilId, IDictionary<Guid, double> habilidadesAvaliadas);
        Task RemoverEsporte(Guid perfilId, Guid esporteId);
    }
}
