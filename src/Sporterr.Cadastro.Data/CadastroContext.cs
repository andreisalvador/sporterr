using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sporterr.Cadastro.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data
{
    public class CadastroContext : DbContext
    {
        private const string CONNECTION_STRING_POSTGRES = "User ID = user;Password=pass;Server=localhost;Port=5432;Database=CadastrosDb;Integrated Security=true;Pooling=true";

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<Quadra> Quadras { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<HistoricoSolicitacao> HistoricosSolicitacoes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(CONNECTION_STRING_POSTGRES);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCriacao").IsModified = false;
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            //if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;

        }
    }
}
