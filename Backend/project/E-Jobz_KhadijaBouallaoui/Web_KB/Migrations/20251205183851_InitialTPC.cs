using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_KB.Migrations
{
    /// <inheritdoc />
    public partial class InitialTPC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "UtilisateursSequence");

            migrationBuilder.CreateTable(
                name: "Base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [UtilisateursSequence]"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoProfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [UtilisateursSequence]"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoProfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitreProfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEnvoie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpediteurId = table.Column<int>(type: "int", nullable: false),
                    DestinataireId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilisateursId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Niveau = table.Column<int>(type: "int", nullable: false),
                    CandidatiId = table.Column<int>(type: "int", nullable: false),
                    CandidatId = table.Column<int>(type: "int", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competence_Candidat_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diplome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ecole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidatiId = table.Column<int>(type: "int", nullable: false),
                    CandidatId = table.Column<int>(type: "int", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formation_Candidat_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OffreEmploi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeContrat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salaire = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatePublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffreEmploi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffreEmploi_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruteur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [UtilisateursSequence]"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoProfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomEntreprise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEntreprise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruteur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruteur_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCommentaire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    UtilisateursId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commentaire_Publication_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSoumission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageMotivation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OffreEmploiId = table.Column<int>(type: "int", nullable: false),
                    CandidatiId = table.Column<int>(type: "int", nullable: false),
                    CandidatId = table.Column<int>(type: "int", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreerPar = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false),
                    DateSupperission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupperimerPar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidature_Candidat_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidature_OffreEmploi_OffreEmploiId",
                        column: x => x.OffreEmploiId,
                        principalTable: "OffreEmploi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_CandidatId",
                table: "Candidature",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_OffreEmploiId",
                table: "Candidature",
                column: "OffreEmploiId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_PublicationId",
                table: "Commentaire",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_UtilisateursId",
                table: "Commentaire",
                column: "UtilisateursId");

            migrationBuilder.CreateIndex(
                name: "IX_Competence_CandidatId",
                table: "Competence",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Formation_CandidatId",
                table: "Formation",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_DestinataireId",
                table: "Message",
                column: "DestinataireId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ExpediteurId",
                table: "Message",
                column: "ExpediteurId");

            migrationBuilder.CreateIndex(
                name: "IX_OffreEmploi_EntrepriseId",
                table: "OffreEmploi",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_UtilisateursId",
                table: "Publication",
                column: "UtilisateursId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruteur_EntrepriseId",
                table: "Recruteur",
                column: "EntrepriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base");

            migrationBuilder.DropTable(
                name: "Candidature");

            migrationBuilder.DropTable(
                name: "Commentaire");

            migrationBuilder.DropTable(
                name: "Competence");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Recruteur");

            migrationBuilder.DropTable(
                name: "OffreEmploi");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "Candidat");

            migrationBuilder.DropTable(
                name: "Entreprise");

            migrationBuilder.DropSequence(
                name: "UtilisateursSequence");
        }
    }
}
