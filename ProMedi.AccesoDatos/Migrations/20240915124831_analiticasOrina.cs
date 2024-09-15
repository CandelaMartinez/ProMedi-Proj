using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProMedi.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class analiticasOrina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnaliticasOrina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Leucositos = table.Column<int>(type: "int", nullable: true),
                    Nitrito = table.Column<int>(type: "int", nullable: true),
                    Hemoglobina = table.Column<int>(type: "int", nullable: true),
                    Proteina = table.Column<int>(type: "int", nullable: true),
                    Creatinina = table.Column<int>(type: "int", nullable: true),
                    Albumina = table.Column<int>(type: "int", nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnaliticasOrina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnaliticasOrina_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AnaliticasOrina_PacienteId",
                table: "AnaliticasOrina",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnaliticasOrina");
        }
    }
}
