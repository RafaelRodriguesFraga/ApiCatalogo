using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder
                .HasKey("Id");

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
              .Property(c => c.Id)
              .HasColumnName("id");

            builder
               .Property(c => c.ImagemUrl)
               .HasColumnName("img_url");

            builder.ToTable("categorias");
        }
    }
}
