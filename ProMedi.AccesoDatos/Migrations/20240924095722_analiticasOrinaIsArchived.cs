using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProMedi.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class analiticasOrinaIsArchived : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "AnaliticasOrina",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "AnaliticasOrina");
        }
    }
}
