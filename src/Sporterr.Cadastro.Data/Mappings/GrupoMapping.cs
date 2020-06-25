using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class GrupoMapping : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.UsuarioCriador)
                   .WithMany(g => g.Grupos)
                   .HasForeignKey(g => g.UsuarioCriadorId);

            builder.ToTable("Grupos");
        }
    }
}
