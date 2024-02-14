using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPrestadores.Migrations
{
    public partial class PrestadorServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,CategoriaServicoId,PrestacaoCidade) VALUES('Marcelo Parisotto', 'Serviço de qualidade','/images/PrestadorPedreiro.jpg',1,'Joaçaba')");
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,CategoriaServicoId,PrestacaoCidade) VALUES('Marcos Parisotto','','/images/PrestadorEncanador.jpg',2,'Luzerna')");
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,CategoriaServicoId,PrestacaoCidade) VALUES('João Dalto','','/images/PrestadorEncanador.jpg',2,'Jaborá')");
            migrationBuilder.Sql("INSERT INTO PrestadorServico(nome,descricao,ImagemUrl,CategoriaServicoId,PrestacaoCidade) VALUES('Silvio Teste','Serviço certo, na hora certa!','/images/PrestadorJardineiro.jpg',3,'Joaçaba')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PrestadorServico");
        }
    }
}
