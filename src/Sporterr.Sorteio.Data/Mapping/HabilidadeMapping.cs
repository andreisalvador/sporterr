using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Sorteio.Domain;

namespace Sporterr.Sorteio.Data.Mapping
{
    public class HabilidadeMapping : IEntityTypeConfiguration<Habilidade>
    {
        public void Configure(EntityTypeBuilder<Habilidade> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasOne(h => h.Esporte)
                .WithMany(e => e.Habilidades)
                .HasForeignKey(h => h.EsporteId);

            builder.ToTable("Habilidades");
        }
    }
}
