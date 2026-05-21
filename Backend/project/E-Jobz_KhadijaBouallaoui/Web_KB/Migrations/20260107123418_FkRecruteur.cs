using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class FkRecruteur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id");
        }
    }
}
