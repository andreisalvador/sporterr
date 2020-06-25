using Sporterr.Cadastro.Domain;
using Sporterr.Core.Data;
using System;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        void AdicionarEmpresa(Empresa empresa);
        void AtualizarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresaPorId(Guid empresaId);

        void AdicionarQuadra(Quadra quadra);
        void AtualizarQuadra(Quadra quadra);
        Task<Quadra> ObterQuadraPorId(Guid quadraId);

        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao solicitacao);
        Task<Solicitacao> ObterSolicitacaoPorId(Guid solicitacaoId);
        Task<Solicitacao> ObterSolicitacaoPorLocacaoEmpresa(Guid locacaoId, Guid empresaId);
    }
}
