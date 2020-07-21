using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly CadastroContext _context;
        public GrupoRepository(CadastroContext context)
        {
            _context = context;
        }

        public void AdicionarGrupo(Grupo grupo)
        {
            _context.Grupos.Add(grupo);
        }

        public void AtualizarGrupo(Grupo grupo)
        {
            _context.Grupos.Update(grupo);
        }

        public async Task<Grupo> ObterGrupoPorId(Guid grupoId)
        {
            return await _context.Grupos.FindAsync(grupoId);
        }

        public void AdicionarMembro(Membro membro)
        {
            _context.Membros.Add(membro);
        }

        public void ExcluirMembro(Membro membro)
        {
            _context.Membros.Remove(membro);
        }

        public async Task<Membro> ObterMembroPorId(Guid membroId)
        {
            return await _context.Membros.FindAsync(membroId);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
