using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Locacoes.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Data.Mappings
{
    public class HistoricoSolicitacaoMapping : IEntityTypeConfiguration<HistoricoSolicitacao>
    {
        public void Configure(EntityTypeBuilder<HistoricoSolicitacao> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasOne(h => h.Solicitacao)
                .WithMany(s => s.Historicos)
                .HasForeignKey(h => h.SolicitacaoId);

            builder.ToTable("HistoricosSolicitacoes");
        }
    }
}
