using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Locacoes.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Data.Mappings
{
    public class SolicitacaoMapping : IEntityTypeConfiguration<Solicitacao>
    {
        public void Configure(EntityTypeBuilder<Solicitacao> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.EmpresaId)
                .IsRequired();

            builder.Property(s => s.QuadraId)
                .IsRequired();

            builder.Property(s => s.UsuarioLocatarioId)
                .IsRequired();

            builder.Property(s => s.DataHoraInicioLocacao)
                .IsRequired();

            builder.Property(s => s.DataHoraFimLocacao)
                .IsRequired();

            builder.HasMany(s => s.Historicos)
                .WithOne(h => h.Solicitacao)
                .HasForeignKey(h => h.SolicitacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Solicitacoes");
        }
    }
}
