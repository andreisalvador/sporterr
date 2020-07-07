using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        public void AtualizarEmpresa(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
        }

        public void AtualizarQuadra(Quadra quadra)
        {
            _context.Quadras.Update(quadra);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid empresaId)
        {   
            return await _context.Empresas.FindAsync(empresaId);
        }

        public async Task<Empresa> ObterEmpresaComQuadrasPorId(Guid empresaId)
        {
            return await _context.Empresas.Include(e => e.Quadras).SingleOrDefaultAsync(e => e.Id.Equals(empresaId));
        }

        public async Task<Quadra> ObterQuadraPorId(Guid quadraId)
        {
            return await _context.Quadras.FindAsync(quadraId);
        }
        
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
