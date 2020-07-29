using Microsoft.EntityFrameworkCore;
using Sporterr.Core.Data;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces;
using Sporterr.Locacoes.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Locacoes.Data
{
    public class LocacoesContext : DbContext, IDbContext
    {
        private const string CONNECTION_STRING_POSTGRES = "User ID = user;Password=pass;Server=localhost;Port=5432;Database=LocacoesDb;Integrated Security=true;Pooling=true";

        private readonly INotificationHandler<DomainNotification> _notificationHandler;
        public LocacoesContext(INotificationHandler<DomainNotification> notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; private set; }
        public DbSet<HistoricoSolicitacao> Historicos { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(CONNECTION_STRING_POSTGRES);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocacoesContext).Assembly);
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
