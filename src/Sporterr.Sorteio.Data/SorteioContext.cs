using Microsoft.EntityFrameworkCore;
using Sporterr.Sorteio.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Data
{
    public class SorteioContext : DbContext
    {
        private const string CONNECTION_STRING_POSTGRES = "User ID = user;Password=pass;Server=localhost;Port=5432;Database=SorteioDb;Integrated Security=true;Pooling=true";

        public DbSet<PerfilHabilidades> PerfisHabilidade { get; set; }
        public DbSet<Esporte> Esportes { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<HabilidadeUsuario> HabilidadesUsuarios { get; set; }
        public DbSet<AvaliacaoHabilidade> AvaliacoesHabilidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(CONNECTION_STRING_POSTGRES);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SorteioContext).Assembly);
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
