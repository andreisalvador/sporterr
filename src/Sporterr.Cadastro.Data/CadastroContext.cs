using Microsoft.EntityFrameworkCore;
using System;

namespace Sporterr.Cadastro.Data
{
    public class CadastroContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroContext).Assembly);
        }
    }
}
