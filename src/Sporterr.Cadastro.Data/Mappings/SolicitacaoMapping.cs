using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class SolicitacaoMapping : IEntityTypeConfiguration<Solicitacao>
    {
        public void Configure(EntityTypeBuilder<Solicitacao> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Empresa)
                .WithMany(e => e.Solicitacoes)
                .HasForeignKey(s => s.EmpresaId);


            builder.ToTable("Solicitacoes");
        }
    }
}
