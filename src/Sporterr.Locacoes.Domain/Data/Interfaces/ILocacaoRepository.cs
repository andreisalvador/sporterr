﻿using Sporterr.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Domain.Data.Interfaces
{
    public interface ILocacaoRepository : IRepository<Locacao>
    {
        void AdicionarLocacao(Locacao locacao);
        void AtualizarLocacao(Locacao locacao);        
        Task<IEnumerable<Locacao>> ObterTodas();
        Task<IEnumerable<Locacao>> ObterPorUsuario(Guid usuarioId);
        Task<Locacao> ObterPorId(Guid locacaoId);
        Task<Locacao> ObterPorSolicitacao(Guid solicitacaoId);
    }
}
