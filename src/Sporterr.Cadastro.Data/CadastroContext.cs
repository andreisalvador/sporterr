using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Data;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Data
{
    public class CadastroContext : DbContext, IDbContext
    {
        private const string CONNECTION_STRING_POSTGRES = "User ID = user;Password=pass;Server=localhost;Port=5432;Database=CadastrosDb;Integrated Security=true;Pooling=true";
        private readonly INotificationHandler<DomainNotification> _notificationHandler;
        public CadastroContext(INotificationHandler<DomainNotification> notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<Quadra> Quadras { get; set; }
        
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
            if (!_notificationHandler.HasNotifications())
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
            return false;
        }
    }
}
