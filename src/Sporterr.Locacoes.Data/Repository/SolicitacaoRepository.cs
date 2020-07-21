using Microsoft.EntityFrameworkCore;
using Sporterr.Locacoes.Domain;
using Sporterr.Locacoes.Domain.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Data.Repository
{
    public class SolicitacaoRepository : ISolicitacaoRepository
    {
        private readonly LocacoesContext _context;

        public SolicitacaoRepository(LocacoesContext context)
        {
            _context = context;
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
        }

        public void AtualizarSolicitacao(Solicitacao solicitacao)
        {
            _context.Historicos.Add(solicitacao.Historicos.Last());
            _context.Solicitacoes.Update(solicitacao);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }


        public async Task<bool> ExisteNoPeriodo(DateTime inicio, DateTime fim)
        {
            return await _context.Solicitacoes.AsQueryable().Where(s => s.DataHoraInicioLocacao >= inicio && fim <= s.DataHoraFimLocacao).AsNoTracking().AnyAsync();
        }

        public async Task<Solicitacao> ObterPorId(Guid solicitacaoId)
        {
            return await _context.Solicitacoes.SingleOrDefaultAsync(s => s.Id.Equals(solicitacaoId));
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
