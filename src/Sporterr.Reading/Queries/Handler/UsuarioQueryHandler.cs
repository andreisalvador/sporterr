using MediatR;
using Sporterr.Reading.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Reading.Queries.Handler
{
    public class UsuarioQueryHandler : IRequestHandler<BuscarSolicitacoesPorUsuarioQuery, SolicitacoesUsuarioReadModel>
    {
        public async Task<SolicitacoesUsuarioReadModel> Handle(BuscarSolicitacoesPorUsuarioQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult<SolicitacoesUsuarioReadModel>(null);
        }
    }
}
