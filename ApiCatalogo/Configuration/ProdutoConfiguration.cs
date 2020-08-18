using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCatalogo.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
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
                   .Property(c => c.Nome)
                   .HasColumnName("nome");

            builder
                  .Property(c => c.Descricao)
                  .HasColumnName("descricao");

            builder
                  .Property(c => c.Preco)
                  .IsRequired()
                  .HasColumnName("preco");

            builder
                  .Property(c => c.ImagemUrl)
                  .HasColumnName("img_url");

            builder
                  .Property(c => c.Estoque)
                  .HasColumnName("estoque");

            builder
                 .Property(c => c.DataCadastro)
                 .HasColumnName("data_cadastro");

            builder
                .HasOne(c => c.Categoria)
                .WithMany(a => a.Produtos)
                .HasForeignKey(a => a.CategoriaId);

            builder.ToTable("produtos");
        }
    }
}
