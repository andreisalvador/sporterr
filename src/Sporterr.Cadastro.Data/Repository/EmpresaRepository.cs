using Microsoft.EntityFrameworkCore;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly CadastroContext _context;

        public EmpresaRepository(CadastroContext context)
        {
            _context = context;
        }

        public void AdicionarEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
        }

        public void AdicionarQuadra(Quadra quadra)
        {
            _context.Quadras.Add(quadra);
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
        }

        public void AtualizarEmpresa(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
        }

        public void AtualizarQuadra(Quadra quadra)
        {
            _context.Quadras.Update(quadra);
        }

        public void AtualizarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Update(solicitacao);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid empresaId)
        {
            return await _context.Empresas.AsQueryable().SingleOrDefaultAsync(empresa => empresa.Id.Equals(empresaId));
        }

        public async Task<Quadra> ObterQuadraPorId(Guid quadraId)
        {
            return await _context.Quadras.AsQueryable().SingleOrDefaultAsync(quadra => quadra.Id.Equals(quadraId));
        }

        public async Task<Solicitacao> ObterSolicitacaoPorId(Guid solicitacaoId)
        {
            return await _context.Solicitacoes.AsQueryable().SingleOrDefaultAsync(solicitacao => solicitacao.Id.Equals(solicitacaoId));
        }

        public async Task<Solicitacao> ObterSolicitacaoPorLocacaoEmpresa(Guid locacaoId, Guid empresaId)
        {
            return await _context.Solicitacoes.Where(solicitacao => solicitacao.LocacaoId.Equals(locacaoId) && solicitacao.EmpresaId.Equals(empresaId)).AsQueryable().SingleOrDefaultAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
