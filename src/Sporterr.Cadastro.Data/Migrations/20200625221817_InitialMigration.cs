using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sporterr.Cadastro.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quadras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    ValorPorTempoLocado = table.Column<decimal>(nullable: false),
                    TempoLocacao = table.Column<TimeSpan>(nullable: false),
                    EmManutencao = table.Column<bool>(nullable: false),
                    TipoEsporteQuadra = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quadras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    LocacaoId = table.Column<Guid>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    QuadraId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosSolicitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    SolicitacaoId = table.Column<Guid>(nullable: false),
                    StatusSolicitacao = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosSolicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosSolicitacao_Solicitacoes_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    GrupoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UsuarioProprietarioId = table.Column<Guid>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    DiasFuncionamento = table.Column<int>(nullable: false),
                    HorarioAbertura = table.Column<TimeSpan>(nullable: false),
                    HorarioFechamento = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Usuarios_UsuarioProprietarioId",
                        column: x => x.UsuarioProprietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UsuarioCriadorId = table.Column<Guid>(nullable: false),
                    NomeGrupo = table.Column<string>(nullable: true),
                    NumeroMaximoMembros = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_Usuarios_UsuarioCriadorId",
                        column: x => x.UsuarioCriadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_UsuarioProprietarioId",
                table: "Empresas",
                column: "UsuarioProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_UsuarioCriadorId",
                table: "Grupos",
                column: "UsuarioCriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosSolicitacao_SolicitacaoId",
                table: "HistoricosSolicitacao",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Quadras_EmpresaId",
                table: "Quadras",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EmpresaId",
                table: "Solicitacoes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoId",
                table: "Usuarios",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quadras_Empresas_EmpresaId",
                table: "Quadras",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Empresas_EmpresaId",
                table: "Solicitacoes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Grupos_GrupoId",
                table: "Usuarios",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Usuarios_UsuarioCriadorId",
                table: "Grupos");

            migrationBuilder.DropTable(
                name: "HistoricosSolicitacao");

            migrationBuilder.DropTable(
                name: "Quadras");

            migrationBuilder.DropTable(
                name: "Solicitacoes");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Grupos");
        }
    }
}
