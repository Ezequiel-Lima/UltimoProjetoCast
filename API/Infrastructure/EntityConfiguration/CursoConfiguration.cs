using API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.EntityConfiguration
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> entity)
        {
            entity.ToTable("Curso", ApplicationDataContext.DEFAULT_SCHEMA);

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);


            entity.Property(e => e.DataInicio).HasColumnType("datetime").IsRequired();

            entity.Property(e => e.DataTermino).HasColumnType("datetime").IsRequired();

            entity.Property(e => e.QuantidadeAlunos)
                .IsRequired();

            entity
                .HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId);

        }
    }
}
