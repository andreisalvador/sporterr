using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class HistoricoSolicitacaoMapping : IEntityTypeConfiguration<HistoricoSolicitacao>
    {
        public void Configure(EntityTypeBuilder<HistoricoSolicitacao> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasOne(h => h.Solicitacao)
                .WithMany(s => s.Historicos)
                .HasForeignKey(h => h.SolicitacaoId);

            builder.ToTable("HistoricosSolicitacao");
        }
    }
}
