using Sporterr.Sorteio.Data.Repository.Interfaces;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Data.Repository
{
    public class PerfilHabilidadesRepository : IPerfilHabilidadesRepository
    {
        private readonly SorteioContext _context;
        public PerfilHabilidadesRepository(SorteioContext context)
        {
            _context = context;
        }
        public void AdicionarPerfilHabilidades(PerfilHabilidades perfilHabilidades)
        {
            _context.PerfisHabilidade.Add(perfilHabilidades);
        }

        public void AtualizarPerfilHabilidades(PerfilHabilidades perfilHabilidades)
        {
            _context.PerfisHabilidade.Update(perfilHabilidades);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
