using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Data.Mapping
{
    public class HabilidadeUsuarioMapping : IEntityTypeConfiguration<HabilidadeUsuario>
    {
        public void Configure(EntityTypeBuilder<HabilidadeUsuario> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasOne(h => h.Esporte);
            builder.HasOne(h => h.Habilidade);

            builder.ToTable("HablidadesUsuarios");
        }
    }
}
