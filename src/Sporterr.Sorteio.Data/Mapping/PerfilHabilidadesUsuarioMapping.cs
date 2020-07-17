using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Data.Mapping
{
    public class PerfilHabilidadesUsuarioMapping : IEntityTypeConfiguration<PerfilHabilidades>
    {
        public void Configure(EntityTypeBuilder<PerfilHabilidades> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.HabilidadesUsario)
                .WithOne(h => h.PerfilHabilidades)
                .HasForeignKey(h => h.PerfilHabilidadesId);                
        }
    }
}
