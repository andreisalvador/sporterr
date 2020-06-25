using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sporterr.Cadastro.Data
{
    public class CadastroContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public CadastroContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroContext).Assembly);
        }
    }
}
