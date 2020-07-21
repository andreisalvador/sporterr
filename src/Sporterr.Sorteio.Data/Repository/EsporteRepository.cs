using Microsoft.EntityFrameworkCore;
using Sporterr.Sorteio.Domain;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Data.Repository
{
    public class EsporteRepository : IEsporteRepository
    {
        private readonly SorteioContext _context;

        public EsporteRepository(SorteioContext context)
        {
            _context = context;
        }

        public void AdicionarEsporte(Esporte esporte)
        {
            _context.Esportes.Add(esporte);
        }

        public void AdicionarHabilidade(Habilidade habilidade)
        {
            _context.Habilidades.Add(habilidade);
        }

        public void AtualizarEsporte(Esporte esporte)
        {
            _context.Esportes.Update(esporte);
        }

        public void AtualizarHabilidade(Habilidade habilidade)
        {
            _context.Habilidades.Update(habilidade);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }

        public async Task<Esporte> ObterEsportePorId(Guid esporteId)
        {
            return await _context.Esportes.FindAsync(esporteId);
        }

        public async Task<Esporte> ObterEsporteComHabilidadesPorId(Guid esporteId)
        {
            return await _context.Esportes.Include(e => e.Habilidades).SingleOrDefaultAsync(e => e.Id.Equals(esporteId));
        }

        public async Task<Habilidade> ObterHabilidadePorId(Guid habilidadeId)
        {
            return await _context.Habilidades.FindAsync(habilidadeId);
        }

        public async Task<IEnumerable<Habilidade>> ObterHabilidadesDoEsporte(Guid esporteId)
        {
            return await _context.Habilidades.AsQueryable().Where(h => h.EsporteId.Equals(esporteId)).AsNoTracking().ToListAsync();
        }

        public void RemoverEsporte(Esporte esporte)
        {
            _context.Esportes.Remove(esporte);
        }

        public void RemoverHabilidade(Habilidade habilidade)
        {
            _context.Habilidades.Remove(habilidade);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
