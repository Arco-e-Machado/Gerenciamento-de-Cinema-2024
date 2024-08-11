using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Controle_de_Cinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fullmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBfilme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    ImagemUrl = table.Column<string>(type: "varchar(600)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBfilme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBSala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDaSala = table.Column<string>(type: "varchar(100)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSala", x => x.Id);
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
                name: "TBAssento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "varchar(100)", nullable: false),
                    sala_Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SessaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAssento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBAssento_TBSala_sala_Id",
                        column: x => x.sala_Id,
                        principalTable: "TBSala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBAssento_TBSessao_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "TBSessao",
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
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.InsertData(
                table: "TBSala",
                columns: new[] { "Id", "Capacidade", "NumeroDaSala" },
                values: new object[,]
                {
                    { 1, 30, "Pequena 01" },
                    { 2, 45, "Pequena 02" },
                    { 3, 80, "Média 01" },
                    { 4, 110, "Média 02" },
                    { 5, 180, "Grande 01" },
                    { 6, 200, "Grande 02" }
                });

            migrationBuilder.InsertData(
                table: "TBfilme",
                columns: new[] { "Id", "Duracao", "Genero", "ImagemUrl", "Nome" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 1, 36, 0, 0), 8, "https://upload.wikimedia.org/wikipedia/en/0/05/Up_%282009_film%29.jpg", "UP - Altas Aventuras" },
                    { 2, new TimeSpan(0, 2, 23, 0, 0), 0, "https://upload.wikimedia.org/wikipedia/en/f/f9/TheAvengers2012Poster.jpg", "Os Vingadores" },
                    { 3, new TimeSpan(0, 2, 7, 0, 0), 1, "https://upload.wikimedia.org/wikipedia/en/e/e7/Jurassic_Park_poster.jpg", "Jurassic Park" },
                    { 4, new TimeSpan(0, 1, 57, 0, 0), 2, "https://upload.wikimedia.org/wikipedia/en/3/35/Biglebowskiposter.jpg", "O Grande Lebowski" },
                    { 5, new TimeSpan(0, 3, 15, 0, 0), 3, "https://upload.wikimedia.org/wikipedia/en/3/38/Schindler%27s_List_movie.jpg", "A Lista de Schindler" },
                    { 6, new TimeSpan(0, 2, 58, 0, 0), 4, "https://upload.wikimedia.org/wikipedia/en/8/87/Ringstrilogyposter.jpg", "O Senhor dos Anéis: A Sociedade do Anel" },
                    { 7, new TimeSpan(0, 2, 2, 0, 0), 5, "https://upload.wikimedia.org/wikipedia/en/6/6b/Exorcist_ver2.jpg", "O Exorcista" },
                    { 8, new TimeSpan(0, 2, 4, 0, 0), 6, "https://upload.wikimedia.org/wikipedia/en/8/86/Posternotebook.jpg", "Diário de uma Paixão" },
                    { 9, new TimeSpan(0, 1, 58, 0, 0), 7, "https://upload.wikimedia.org/wikipedia/en/8/86/The_Silence_of_the_Lambs_poster.jpg", "O Silêncio dos Inocentes" },
                    { 10, new TimeSpan(0, 2, 35, 0, 0), 9, "https://upload.wikimedia.org/wikipedia/en/8/8d/Gladiator_ver1.jpg", "Gladiador" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAssento_sala_Id",
                table: "TBAssento",
                column: "sala_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBAssento_SessaoId",
                table: "TBAssento",
                column: "SessaoId");

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
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "TBIngresso");

            migrationBuilder.DropTable(
                name: "TBAssento");

            migrationBuilder.DropTable(
                name: "TBSessao");

            migrationBuilder.DropTable(
                name: "TBSala");

            migrationBuilder.DropTable(
                name: "TBfilme");
        }
    }
}
