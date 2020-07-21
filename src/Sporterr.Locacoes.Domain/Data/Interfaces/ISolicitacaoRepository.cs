using Sporterr.Core.Data;
using System;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Domain.Data.Interfaces
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao solicitacao);
        Task<Solicitacao> ObterPorId(Guid solicitacaoId);
        Task<bool> ExisteNoPeriodo(DateTime inicio, DateTime fim);
    }
}
