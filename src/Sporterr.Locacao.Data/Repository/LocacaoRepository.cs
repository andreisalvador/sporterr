using Microsoft.EntityFrameworkCore;
using Sporterr.Locacoes.Data.Repository.Interfaces;
using Sporterr.Locacoes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Data.Repository
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly LocacoesContext _context;
        public LocacaoRepository(LocacoesContext context)
        {
            _context = context;
        }

        public void AdicionarLocacao(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
        }

        public void AtualizarLocacao(Locacao locacao)
        {
            _context.Locacoes.Update(locacao);
        }

        public async Task<bool> Commit()
        {
            //Por enquanto vai ficar assimr.
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Locacao> ObterPorId(Guid locacaoId)
        {
            return await _context.Locacoes.FindAsync(locacaoId);
        }

        public async Task<IEnumerable<Locacao>> ObterPorUsuario(Guid usuarioId)
        {
            return await _context.Locacoes.AsNoTracking().Where(l => l.UsuarioLocatarioId == usuarioId).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> ObterTodas()
        {
            return await _context.Locacoes.AsNoTracking().ToListAsync();
        }        
    }
}
