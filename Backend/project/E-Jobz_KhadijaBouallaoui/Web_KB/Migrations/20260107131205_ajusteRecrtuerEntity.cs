using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class ajusteRecrtuerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEntreprise",
                table: "Recruteur");

            migrationBuilder.DropColumn(
                name: "NomEntreprise",
                table: "Recruteur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEntreprise",
                table: "Recruteur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomEntreprise",
                table: "Recruteur",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
