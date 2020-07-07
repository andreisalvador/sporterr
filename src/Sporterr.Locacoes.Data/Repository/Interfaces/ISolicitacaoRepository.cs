using Sporterr.Core.Data;
using Sporterr.Locacoes.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Data.Repository.Interfaces
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao solicitacao);
        Task<Solicitacao> ObterPorId(Guid solicitacaoId);
        Task<bool> ExisteNoPeriodo(DateTime inicio, DateTime fim);
    }
}
