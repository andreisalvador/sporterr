using Sporterr.Core.Data;
using Sporterr.Locacoes.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Data.Repository.Interfaces
{
    public interface ILocacaoRepository : IRepository<Locacao>
    {
        void AdicionarLocacao(Locacao locacao);
        void AtualizarLocacao(Locacao locacao);
        void ExcluirLocacao(Locacao locacao);
        Task<IEnumerable<Locacao>> ObterTodas();
        Task<IEnumerable<Locacao>> ObterPorUsuario(Guid usuarioId);
        Task<Locacao> ObterPorId(Guid locacaoId);
        Task<Locacao> ObterNoPeriodo(DateTime inicio, DateTime fim);
        Task<bool> ExisteNoPeriodo(DateTime inicio, DateTime fim);
    }
}
