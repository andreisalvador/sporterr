using Sporterr.Cadastro.Domain;
using Sporterr.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        void AdicionarEmpresa(Empresa usuario);
        void AtualizarEmpresa(Empresa usuario);
        Task<Empresa> ObterEmpresaPorId(Guid id);

        void AdicionarQuadra(Quadra quadra);
        void AtualizarQuadra(Quadra quadra);
        Task<Quadra> ObterQuadraPorId(Guid id);

        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao usuario);
        Task<Solicitacao> ObterSolicitacaoPorId(Guid id);
    }
}
