using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPrestadores.Migrations
{
    public partial class PrestadorServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,TipoServicoId) VALUES('Marcelo Parisotto', 'Serviço de qualidade','/images/PrestadorPedreiro.jpg',1)");
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,TipoServicoId) VALUES('Marcos Parisotto','','/images/PrestadorEncanador.jpg',2)");
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,TipoServicoId) VALUES('João Dalto','','/images/PrestadorEncanador.jpg',2)");
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,TipoServicoId) VALUES('Silvio Teste','Serviço certo, na hora certa!','/images/PrestadorJardineiro.jpg',3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PrestadorServico");
        }
    }
}
