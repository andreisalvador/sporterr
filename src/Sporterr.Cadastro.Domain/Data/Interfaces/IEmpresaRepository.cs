using Sporterr.Core.Data;
using System;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Domain.Data.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        void AdicionarEmpresa(Empresa empresa);
        void AtualizarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresaPorId(Guid empresaId);
        Task<Empresa> ObterEmpresaComQuadrasPorId(Guid empresaId);

        void AdicionarQuadra(Quadra quadra);
        void AtualizarQuadra(Quadra quadra);
        Task<Quadra> ObterQuadraPorId(Guid quadraId);
    }
}
