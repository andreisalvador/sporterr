using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Data.Mapping
{
    public class AvaliacaoHabilidadeMapping : IEntityTypeConfiguration<AvaliacaoHabilidade>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoHabilidade> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.HabilidadeUsuario)
                .WithMany(h => h.Avaliacoes)
                .HasForeignKey(a => a.HabilidadeUsuarioId);
        }
    }
}
