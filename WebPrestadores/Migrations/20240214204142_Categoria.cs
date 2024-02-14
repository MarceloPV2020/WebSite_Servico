using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPrestadores.Migrations
{
    public partial class Categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CategoriaServico(nome,descricao,ImagemUrl) VALUES('Pedreiro','Profissional que atual no ramo de construções e reformas de imóveis','/images/pedreiro.jpg')");
            migrationBuilder.Sql("INSERT INTO CategoriaServico(nome,descricao,ImagemUrl) VALUES('Jardineiro','Profissional que atual em limpeza e manuetnção de jardins e lotes','/images/jardineiro.jpg')");
            migrationBuilder.Sql("INSERT INTO CategoriaServico(nome,descricao,ImagemUrl) VALUES('Encanador','Profissional que atual em manutenção de encanamentos','/images/encanador.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CategoriaServico");
        }
    }
}
