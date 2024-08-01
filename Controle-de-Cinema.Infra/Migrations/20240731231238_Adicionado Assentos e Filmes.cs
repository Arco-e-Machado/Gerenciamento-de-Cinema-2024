using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controle_de_Cinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoAssentoseFilmes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TBsala",
                table: "TBsala");

            migrationBuilder.RenameTable(
                name: "TBsala",
                newName: "TBSala");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBSala",
                table: "TBSala",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TBAssento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "varchar(100)", nullable: false),
                    Sala_Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAssento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBAssento_TBSala_Sala_Id",
                        column: x => x.Sala_Id,
                        principalTable: "TBSala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBfilme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBfilme", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAssento_Sala_Id",
                table: "TBAssento",
                column: "Sala_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAssento");

            migrationBuilder.DropTable(
                name: "TBfilme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBSala",
                table: "TBSala");

            migrationBuilder.RenameTable(
                name: "TBSala",
                newName: "TBsala");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBsala",
                table: "TBsala",
                column: "Id");
        }
    }
}
