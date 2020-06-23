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

            builder.ToTable("Solicitacoes");
        }
    }
}
