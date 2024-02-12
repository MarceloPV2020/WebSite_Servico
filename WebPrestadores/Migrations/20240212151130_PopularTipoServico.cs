using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPrestadores.Migrations
{
    public partial class PopularTipoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TipoServico(nome,descricao) VALUES('Pedreiro','Profissional que atual no ramo de construções e reformas de imóveis')");
            migrationBuilder.Sql("INSERT INTO TipoServico(nome,descricao) VALUES('Jardineiro','Profissional que atual em limpeza e manuetnção de jardins e lotes')");
            migrationBuilder.Sql("INSERT INTO TipoServico(nome,descricao) VALUES('Encanador','Profissional que atual em manutenção de encanamentos')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TipoServico");
        }
    }
}
