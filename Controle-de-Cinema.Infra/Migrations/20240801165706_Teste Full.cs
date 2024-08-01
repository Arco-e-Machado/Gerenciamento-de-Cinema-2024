using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controle_de_Cinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TesteFull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBPessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBSessao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filme_Id = table.Column<int>(type: "int", nullable: false),
                    Sala_Id = table.Column<int>(type: "int", nullable: false),
                    InicioDaSessao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FimDaSessao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSessao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBSessao_TBSala_Sala_Id",
                        column: x => x.Sala_Id,
                        principalTable: "TBSala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBSessao_TBfilme_Filme_Id",
                        column: x => x.Filme_Id,
                        principalTable: "TBfilme",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TBIngresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sessao_Id = table.Column<int>(type: "int", nullable: false),
                    AssentoId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBIngresso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBIngresso_TBAssento_AssentoId",
                        column: x => x.AssentoId,
                        principalTable: "TBAssento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBIngresso_TBSessao_Sessao_Id",
                        column: x => x.Sessao_Id,
                        principalTable: "TBSessao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_AssentoId",
                table: "TBIngresso",
                column: "AssentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_Sessao_Id",
                table: "TBIngresso",
                column: "Sessao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBSessao_Filme_Id",
                table: "TBSessao",
                column: "Filme_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBSessao_Sala_Id",
                table: "TBSessao",
                column: "Sala_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBIngresso");

            migrationBuilder.DropTable(
                name: "TBPessoa");

            migrationBuilder.DropTable(
                name: "TBSessao");
        }
    }
}
