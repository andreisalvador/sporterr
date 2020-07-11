using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Sorteio.Domain;
using System;

namespace Sporterr.Sorteio.Data.Mapping
{
    public class EsporteMapping : IEntityTypeConfiguration<Esporte>
    {
        public void Configure(EntityTypeBuilder<Esporte> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Habilidades)
                .WithOne(h => h.Esporte)
                .HasForeignKey(h => h.EsporteId);

            builder.HasIndex(e => e.TipoEsporte)
                .IsUnique();

            builder.ToTable("Esportes");
        }
    }
}
