using Microsoft.EntityFrameworkCore;
using Sporterr.Sorteio.Domain;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> Existe(Guid perfilId)
        {
            return await _context.PerfisHabilidade.AnyAsync(p => p.Id.Equals(perfilId));
        }

        public async Task<bool> ExisteParaUsuario(Guid usuarioId)
        {
            return await _context.PerfisHabilidade.AnyAsync(p => p.UsuarioId.Equals(usuarioId));
        }

        public async Task<PerfilHabilidades> ObterPorId(Guid perfilId)
        {
            return await _context.PerfisHabilidade.FindAsync(perfilId);
        }
        public async Task<PerfilHabilidades> ObterPorIdComHabilidades(Guid perfilId)
        {
            return await _context.PerfisHabilidade
                .Include(p => p.HabilidadesUsuario)
                .ThenInclude(h => h.Avaliacoes)
                .SingleOrDefaultAsync(p => p.Id.Equals(perfilId));
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
