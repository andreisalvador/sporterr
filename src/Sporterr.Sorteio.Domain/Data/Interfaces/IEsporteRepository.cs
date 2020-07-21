using Sporterr.Core.Data;
using Sporterr.Core.Enums;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Domain.Data.Interfaces
{
    public interface IEsporteRepository : IRepository<Esporte>
    {
        void AdicionarEsporte(Esporte esporte);
        void RemoverEsporte(Esporte esporte);
        void AtualizarEsporte(Esporte esporte);
        Task<Esporte> ObterEsportePorId(Guid esporteId);
        Task<Esporte> ObterEsporteComHabilidadesPorId(Guid esporteId);

        void AdicionarHabilidade(Habilidade habilidade);
        void RemoverHabilidade(Habilidade habilidade);
        void AtualizarHabilidade(Habilidade habilidade);
        Task<Habilidade> ObterHabilidadePorId(Guid habilidadeId);
        Task<IEnumerable<Habilidade>> ObterHabilidadesDoEsporte(Guid esporteId);
    }
}
