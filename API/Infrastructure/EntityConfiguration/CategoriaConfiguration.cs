using API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.EntityConfiguration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> entity)
        {
            entity.ToTable("Categoria", ApplicationDataContext.DEFAULT_SCHEMA);

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);
        }
    }
}
