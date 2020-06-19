using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.UsuarioProprietario)
                   .WithMany(e => e.Empresas)
                   .HasForeignKey(e => e.UsuarioProprietarioId);

            builder.ToTable("Empresas");
        }
    }
}
