using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public void AdicionarEmpresa(Empresa usuario)
        {
            throw new NotImplementedException();
        }

        public void AdicionarQuadra(Quadra quadra)
        {
            throw new NotImplementedException();
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            throw new NotImplementedException();
        }

        public void AtualizarEmpresa(Empresa usuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarQuadra(Quadra quadra)
        {
            throw new NotImplementedException();
        }

        public void AtualizarSolicitacao(Solicitacao usuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Commit()
        {
            throw new NotImplementedException();
        }

        public Task<Empresa> ObterEmpresaPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Quadra> ObterQuadraPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Solicitacao> ObterSolicitacaoPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Solicitacao> ObterSolicitacaoPorLocacaoEmpresa(Guid locacaoId, Guid empresaId)
        {
            throw new NotImplementedException();
        }
    }
}
