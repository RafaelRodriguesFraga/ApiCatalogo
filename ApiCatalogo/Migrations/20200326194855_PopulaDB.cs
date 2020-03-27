using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCatalogo.Migrations
{
    public partial class PopulaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Bebidas', " +
                "'https://www.bebidasfamosas.com.br/blog/wp-content/uploads/2015/08/11234966_1014175895282875_8363826705020840929_n.png')");

             migrationBuilder.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Lanches', " +
                "'https://images2.nogueirense.com.br/wp-content/uploads/2018/08/whatsapp-image-2018-08-16-at-19-59-49-1534878741.jpeg')");

             migrationBuilder.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Sobremesas', " +
                "'https://catracalivre.com.br/wp-content/uploads/2019/12/sobremesas-natal.jpg')");

            migrationBuilder.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                " VALUES ('Coca-Cola Diet', 'Refrigerante de Cola 350 ml', '5.45', 'https://farmaciaindiana.vteximg.com.br/arquivos/ids/219828-1000-1000/7894900700077.jpg?v=637123739848470000'," +
                "'50', now(), (SELECT id from Categorias WHERE Nome = 'Bebidas'))");

            migrationBuilder.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                " VALUES ('Lanche de Atum', 'Lanche de Atum com maiose', '8.50', 'https://s2.glbimg.com/9zNRSECkqz0FCnFY1WgK6JGswwM=/696x390/smart/filters:cover():strip_icc()/s.glbimg.com/po/rc/media/2012/06/13/17/14/17/815/sanduiche_atum_light.jpg'," +
                "'10', now(), (SELECT id from Categorias WHERE Nome = 'Lanches'))");

            migrationBuilder.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "VALUES ('Pudim', 'Pudim de leite condensado', '6.75', 'https://img.itdg.com.br/tdg/images/recipes/000/031/593/318825/318825_original.jpg?mode=crop&width=710&height=400'," +
                "'20', now(), (SELECT id from Categorias WHERE Nome = 'Sobremesas'))");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
