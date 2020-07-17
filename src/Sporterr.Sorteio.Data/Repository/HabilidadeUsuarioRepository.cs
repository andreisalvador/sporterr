using Sporterr.Sorteio.Data.Repository.Interfaces;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Data.Repository
{
    public class HabilidadeUsuarioRepository : IHabilidadeUsuarioRepository
    {
        private readonly SorteioContext _context;
        public HabilidadeUsuarioRepository(SorteioContext context)
        {
            _context = context;
        }

        public void AdicionarAvaliacaoHabilidade(AvaliacaoHabilidade avaliacaoHabilidade)
        {
            _context.AvaliacoesHabilidade.Add(avaliacaoHabilidade);
        }

        public void AdicionarHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario)
        {
            _context.HabilidadesUsuarios.AddRange(habilidadesUsuario);
        }

        public void AdicionarHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario)
        {
            _context.HabilidadesUsuarios.Add(habilidadeUsuario);
        }

        public void AtualizarHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario)
        {
            _context.HabilidadesUsuarios.UpdateRange(habilidadesUsuario);
        }

        public void AtualizarHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario)
        {
            _context.HabilidadesUsuarios.Update(habilidadeUsuario);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }

        public void RemoverHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario)
        {
            _context.HabilidadesUsuarios.RemoveRange(habilidadesUsuario);
        }

        public void RemoverHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario)
        {
            _context.HabilidadesUsuarios.Remove(habilidadeUsuario);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
