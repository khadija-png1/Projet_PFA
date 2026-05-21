using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class bdrepair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_Candidat_CandidatId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_OffreEmploi_OffreEmploiId",
                table: "Candidature");

            migrationBuilder.DropIndex(
                name: "IX_Candidature_CandidatId",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "CandidatId",
                table: "Candidature");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_CandidatiId",
                table: "Candidature",
                column: "CandidatiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_Candidat_CandidatiId",
                table: "Candidature",
                column: "CandidatiId",
                principalTable: "Candidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_OffreEmploi_OffreEmploiId",
                table: "Candidature",
                column: "OffreEmploiId",
                principalTable: "OffreEmploi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_Candidat_CandidatiId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_OffreEmploi_OffreEmploiId",
                table: "Candidature");

            migrationBuilder.DropIndex(
                name: "IX_Candidature_CandidatiId",
                table: "Candidature");

            migrationBuilder.AddColumn<int>(
                name: "CandidatId",
                table: "Candidature",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_CandidatId",
                table: "Candidature",
                column: "CandidatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_Candidat_CandidatId",
                table: "Candidature",
                column: "CandidatId",
                principalTable: "Candidat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_OffreEmploi_OffreEmploiId",
                table: "Candidature",
                column: "OffreEmploiId",
                principalTable: "OffreEmploi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
