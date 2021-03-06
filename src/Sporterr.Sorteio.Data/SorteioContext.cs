﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Sporterr.Core.Data;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Handler;
using Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces;
using Sporterr.Sorteio.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Data
{
    public class SorteioContext : DbContext, IDbContext
    {        
        private readonly DomainNotificationHandler _notificationHandler;

        public SorteioContext(INotificationHandler<DomainNotification> notificationHandler, DbContextOptions<SorteioContext> options) : base(options)
        {
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        public DbSet<PerfilHabilidades> PerfisHabilidade { get; set; }
        public DbSet<Esporte> Esportes { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<HabilidadeUsuario> HabilidadesUsuarios { get; set; }
        public DbSet<AvaliacaoHabilidade> AvaliacoesHabilidade { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SorteioContext).Assembly);
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
