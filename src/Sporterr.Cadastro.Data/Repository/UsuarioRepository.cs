﻿using Microsoft.EntityFrameworkCore;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;
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
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid empresaId)
        {
            return await _context.Empresas.SingleOrDefaultAsync(e => e.Id.Equals(empresaId));
        }

        public async Task<Grupo> ObterGrupoPorId(Guid grupoId)
        {
            return await _context.Grupos.SingleOrDefaultAsync(g => g.Id.Equals(grupoId));
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid usuarioId)
        {
            return await _context.Usuarios.AsQueryable().SingleOrDefaultAsync(u => u.Id.Equals(usuarioId));
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
