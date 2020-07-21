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

            builder.Property(e => e.Cnpj).HasConversion(new ValueConverter<Cnpj, string>(
                x => x.Value,
                x => Cnpj.Parse(x)
                )).HasColumnName("Cnpj");
            builder.ToTable("Empresas");
        }
    }
}
