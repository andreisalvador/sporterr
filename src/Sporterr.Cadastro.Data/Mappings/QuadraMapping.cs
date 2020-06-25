using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sporterr.Cadastro.Domain;

namespace Sporterr.Cadastro.Data.Mappings
{
    public class QuadraMapping : IEntityTypeConfiguration<Quadra>
    {
        public void Configure(EntityTypeBuilder<Quadra> builder)
        {
            builder.HasKey(q => q.Id);

            builder.HasOne(x => x.Empresa)
                   .WithMany(q => q.Quadras)
                   .HasForeignKey(q => q.EmpresaId);

            builder.ToTable("Quadras");
        }
    }
}
