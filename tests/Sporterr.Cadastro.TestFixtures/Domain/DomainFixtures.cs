using Sporterr.Cadastro.TestFixtures.Domain.Fixtures;
using System;

namespace Sporterr.Cadastro.TestFixtures.Domain
{
    public class DomainFixtures : IDisposable
    {
        private readonly Lazy<EmpresaFixture> empresaFixture;
        private readonly Lazy<UsuarioFixture> usuarioFixture;
        private readonly Lazy<GrupoFixture> grupoFixture;
        private readonly Lazy<QuadraFixture> quadraFixture;
        private readonly Lazy<MembroFixture> membroFixture;

        public EmpresaFixture Empresa => empresaFixture.Value;
        public UsuarioFixture Usuario => usuarioFixture.Value;
        public GrupoFixture Grupo => grupoFixture.Value;
        public QuadraFixture Quadra => quadraFixture.Value;
        public MembroFixture Membro => membroFixture.Value;

        public DomainFixtures()
        {
            empresaFixture = new Lazy<EmpresaFixture>();
            usuarioFixture = new Lazy<UsuarioFixture>();
            grupoFixture = new Lazy<GrupoFixture>();
            quadraFixture = new Lazy<QuadraFixture>();
            membroFixture = new Lazy<MembroFixture>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
