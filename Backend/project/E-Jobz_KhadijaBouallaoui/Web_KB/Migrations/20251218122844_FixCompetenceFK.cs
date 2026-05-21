using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class FixCompetenceFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_Candidat_CandidatId",
                table: "Competence");

            migrationBuilder.DropForeignKey(
                name: "FK_Formation_Candidat_CandidatId",
                table: "Formation");

            migrationBuilder.DropIndex(
                name: "IX_Formation_CandidatId",
                table: "Formation");

            migrationBuilder.DropIndex(
                name: "IX_Competence_CandidatId",
                table: "Competence");

            migrationBuilder.DropColumn(
                name: "CandidatId",
                table: "Formation");

            migrationBuilder.DropColumn(
                name: "CandidatId",
                table: "Competence");

            migrationBuilder.CreateIndex(
                name: "IX_Formation_CandidatiId",
                table: "Formation",
                column: "CandidatiId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_CandidatiId",
                table: "Competence",
                column: "CandidatiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_Candidat_CandidatiId",
                table: "Competence",
                column: "CandidatiId",
                principalTable: "Candidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Formation_Candidat_CandidatiId",
                table: "Formation",
                column: "CandidatiId",
                principalTable: "Candidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_Candidat_CandidatiId",
                table: "Competence");

            migrationBuilder.DropForeignKey(
                name: "FK_Formation_Candidat_CandidatiId",
                table: "Formation");

            migrationBuilder.DropIndex(
                name: "IX_Formation_CandidatiId",
                table: "Formation");

            migrationBuilder.DropIndex(
                name: "IX_Competence_CandidatiId",
                table: "Competence");

            migrationBuilder.AddColumn<int>(
                name: "CandidatId",
                table: "Formation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidatId",
                table: "Competence",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Formation_CandidatId",
                table: "Formation",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_CandidatId",
                table: "Competence",
                column: "CandidatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_Candidat_CandidatId",
                table: "Competence",
                column: "CandidatId",
                principalTable: "Candidat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Formation_Candidat_CandidatId",
                table: "Formation",
                column: "CandidatId",
                principalTable: "Candidat",
                principalColumn: "Id");
        }
    }
}
