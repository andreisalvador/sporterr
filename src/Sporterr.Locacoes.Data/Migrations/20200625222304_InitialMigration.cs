using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sporterr.Locacoes.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UsuarioLocatarioId = table.Column<Guid>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    QuadraId = table.Column<Guid>(nullable: true),
                    ValorPorTempoLocadoQuadra = table.Column<decimal>(nullable: true),
                    TempoLocacaoQuadra = table.Column<TimeSpan>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    DataHoraInicioLocacao = table.Column<DateTime>(nullable: false),
                    DataHoraFimLocacao = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SolicitacaoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locacoes");
        }
    }
}
