using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CastCurso");

            migrationBuilder.CreateTable(
                name: "Categoria",
                schema: "CastCurso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                schema: "CastCurso",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "datetime", nullable: false),
                    QuantidadeAlunos = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "CastCurso",
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_CategoriaId",
                schema: "CastCurso",
                table: "Curso",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curso",
                schema: "CastCurso");

            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "CastCurso");
        }
    }
}
