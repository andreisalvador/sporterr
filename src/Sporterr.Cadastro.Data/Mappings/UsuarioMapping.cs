using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Empresas)
                .WithOne(u => u.UsuarioProprietario)
                .HasForeignKey(u => u.UsuarioProprietarioId);

            builder.HasMany(u => u.Grupos)
                .WithOne(u => u.UsuarioCriador)
                .HasForeignKey(u => u.UsuarioCriadorId);

            builder.ToTable("Usuarios");
        }
    }
}
