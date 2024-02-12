using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPrestadores.Migrations
{
    public partial class PopularUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO usuario(nome,prestador,TipoServicoId) VALUES('Marcelo Parisotto',1,1)");
            migrationBuilder.Sql("INSERT INTO usuario(nome,prestador,TipoServicoId) VALUES('Marcelo Parisotto',1,2)");
            migrationBuilder.Sql("INSERT INTO usuario(nome,prestador,TipoServicoId) VALUES('João Dalto',1,2)");
            migrationBuilder.Sql("INSERT INTO usuario(nome,prestador,TipoServicoId) VALUES('Silvio Teste',1,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM usuario");
        }
    }
}
