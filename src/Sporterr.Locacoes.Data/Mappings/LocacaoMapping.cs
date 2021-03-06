﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Locacoes.Domain;

namespace Sporterr.Locacoes.Data.Mappings
{
    public class LocacaoMapping : IEntityTypeConfiguration<Locacao>
    {
        public void Configure(EntityTypeBuilder<Locacao> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.UsuarioLocatarioId)
                .IsRequired();

            builder.Property(l => l.SolicitacaoId)
              .IsRequired();

            builder.Property(l => l.EmpresaId)
                .IsRequired();

            builder.ToTable("Locacoes");
        }
    }
}
