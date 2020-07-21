using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CadastroContext _context;
        public UsuarioRepository(CadastroContext context)
        {
            _context = context;
        }

        public void AdicionarEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
        }

        public void AdicionarGrupo(Grupo grupo)
        {
            _context.Grupos.Add(grupo);
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void AtualizarEmpresa(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
        }

        public void AtualizarGrupo(Grupo grupo)
        {
            _context.Grupos.Update(grupo);
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public async Task<bool> Commit()
        {
            return await _context.CommitAsync();
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid empresaId)
        {
            return await _context.Empresas.FindAsync(empresaId);
        }

        public async Task<Grupo> ObterGrupoPorId(Guid grupoId)
        {
            return await _context.Grupos.FindAsync(grupoId);
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
