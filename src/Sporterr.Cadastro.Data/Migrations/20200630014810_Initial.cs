using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sporterr.Cadastro.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
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
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UsuarioProprietarioId = table.Column<Guid>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    DiasFuncionamento = table.Column<int>(nullable: false),
                    HorarioAbertura = table.Column<TimeSpan>(nullable: false),
                    HorarioFechamento = table.Column<TimeSpan>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
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
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UsuarioCriadorId = table.Column<Guid>(nullable: false),
                    NomeGrupo = table.Column<string>(nullable: true),
                    NumeroMaximoMembros = table.Column<short>(nullable: false),
                    QuantidadeMembros = table.Column<short>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Quadras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    ValorPorTempoLocado = table.Column<decimal>(nullable: false),
                    TempoLocacao = table.Column<TimeSpan>(nullable: false),
                    EmManutencao = table.Column<bool>(nullable: false),
                    TipoEsporteQuadra = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quadras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quadras_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembrosGrupos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembrosGrupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembrosGrupos_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembrosGrupos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    LocacaoId = table.Column<Guid>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    QuadraId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacoes_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitacoes_Quadras_QuadraId",
                        column: x => x.QuadraId,
                        principalTable: "Quadras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosSolicitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "IX_MembrosGrupos_GrupoId",
                table: "MembrosGrupos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_MembrosGrupos_UsuarioId",
                table: "MembrosGrupos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Quadras_EmpresaId",
                table: "Quadras",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EmpresaId",
                table: "Solicitacoes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_QuadraId",
                table: "Solicitacoes",
                column: "QuadraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosSolicitacao");

            migrationBuilder.DropTable(
                name: "MembrosGrupos");

            migrationBuilder.DropTable(
                name: "Solicitacoes");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Quadras");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
