using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sporterr.Cadastro.Domain;

namespace Sporterr.Cadastro.Data
{
    public class CadastroContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Quadra> Quadras { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<HistoricoSolicitacao> HistoricosSolicitacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroContext).Assembly);
        }
    }
}
