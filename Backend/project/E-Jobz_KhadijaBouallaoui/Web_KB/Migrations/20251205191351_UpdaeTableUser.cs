using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class UpdaeTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Base",
                table: "Base");

            migrationBuilder.RenameTable(
                name: "Base",
                newName: "Utilisateur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilisateur",
                table: "Utilisateur",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilisateur",
                table: "Utilisateur");

            migrationBuilder.RenameTable(
                name: "Utilisateur",
                newName: "Base");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Base",
                table: "Base",
                column: "Id");
        }
    }
}
