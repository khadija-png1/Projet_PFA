using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class propre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffreEmploi_Entreprise_EntrepriseId",
                table: "OffreEmploi");

            migrationBuilder.AddForeignKey(
                name: "FK_OffreEmploi_Entreprise_EntrepriseId",
                table: "OffreEmploi",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffreEmploi_Entreprise_EntrepriseId",
                table: "OffreEmploi");

            migrationBuilder.AddForeignKey(
                name: "FK_OffreEmploi_Entreprise_EntrepriseId",
                table: "OffreEmploi",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
