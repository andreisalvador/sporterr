using Microsoft.EntityFrameworkCore;
using Sporterr.Locacoes.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Data
{
    public class LocacoesContext : DbContext
    {
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocacoesContext).Assembly);
        }
    }
}
