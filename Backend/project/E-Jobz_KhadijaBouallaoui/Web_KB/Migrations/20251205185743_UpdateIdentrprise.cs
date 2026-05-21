using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentrprise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur");

            migrationBuilder.AlterColumn<int>(
                name: "EntrepriseId",
                table: "Recruteur",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur");

            migrationBuilder.AlterColumn<int>(
                name: "EntrepriseId",
                table: "Recruteur",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruteur_Entreprise_EntrepriseId",
                table: "Recruteur",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
