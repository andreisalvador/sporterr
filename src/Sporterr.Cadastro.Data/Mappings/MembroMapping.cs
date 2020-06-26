using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class MembroMapping : IEntityTypeConfiguration<Membro>
    {
        public void Configure(EntityTypeBuilder<Membro> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.Grupo)
                .WithMany(g => g.Membros)
                .HasForeignKey(m => m.GrupoId);

            builder.ToTable("MembrosGrupos");
        }
    }
}
