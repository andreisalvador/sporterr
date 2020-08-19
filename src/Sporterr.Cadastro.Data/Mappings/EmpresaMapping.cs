using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sporterr.Cadastro.Domain;

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

            builder.OwnsOne(e => e.Cnpj, c => {
                c.Property(cnpj => cnpj.Value).HasColumnName("Cnpj");
            });

            builder.ToTable("Empresas");
        }
    }
}
