using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Web_KB.Entity;

namespace Web_KB.Data
{
    public class AppDbContext : DbContext
    {
        // Le constructeur qui prend les options
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
     
        // DbSets pour toutes les entités
        public DbSet<Utilisateurs> Utilisateurs { get; set; }
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<Recruteur> Recruteurs { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<OffreEmploi> OffresEmploi { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Messages (déjà présent)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Expediteur)
                .WithMany(u => u.MessagesEnvoyes)
                .HasForeignKey(m => m.ExpediteurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Destinataire)
                .WithMany(u => u.MessagesRecus)
                .HasForeignKey(m => m.DestinataireId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Ajouter : Publications <-> Utilisateurs
            modelBuilder.Entity<Publication>()
                .HasOne(p => p.Utilisateurs)                // propriété de navigation dans Publication
                .WithMany(u => u.Publication)             // collection dans Utilisateur
                .HasForeignKey(p => p.UtilisateursId)
                .OnDelete(DeleteBehavior.Cascade);         // choix : supprimer publications lors de suppression d'utilisateur

            // --- Ajouter : Commentaires <-> Publications
            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Publication)                // commentaire -> publication
                .WithMany(p => p.Commentaire)             // publication -> collection commentaires
                .HasForeignKey(c => c.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);         // si tu supprimes une publication, supprimer ses commentaires

            // --- Ajouter : Commentaires <-> Utilisateurs (pas de cascade pour éviter multiple cascade paths)
            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Utilisateurs)                // commentaire -> utilisateur (auteur)
                .WithMany(u => u.Commentaire)             // utilisateur -> collection commentaires
                .HasForeignKey(c => c.UtilisateursId)
                .OnDelete(DeleteBehavior.Restrict);

            // IMPORTANT : empêche cascade directe depuis Utilisateur vers Commentaire

            // --- Conversion enum Statut en string ---
            modelBuilder.Entity<OffreEmploi>()
                .Property(o => o.Statut)
                .HasConversion<string>();
          
            modelBuilder.Entity<Candidature>(entity =>
            {
                entity.ToTable("Candidature");

                // Enum → string
                entity.Property(c => c.Statut)
                      .HasConversion<string>()
                      .IsRequired();

                // FK vers OffreEmploi
                entity.HasOne(c => c.OffreEmploi)
                      .WithMany(o => o.Candidature)   
                      .HasForeignKey(c => c.OffreEmploiId)
                      .OnDelete(DeleteBehavior.Restrict);

                // FK vers Candidat
                entity.HasOne(c => c.Candidat)
                      .WithMany(ca => ca.Candidature)
                      .HasForeignKey(c => c.CandidatiId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Utilisateurs>()
               .Property(u => u.Role)
               .HasConversion<string>();
            // Table principale pour Utilisateurs
            modelBuilder.Entity<Utilisateurs>().UseTpcMappingStrategy();

            // Table spécifique pour Candidat avec ses colonnes
            modelBuilder.Entity<Candidat>().ToTable("Candidat");

            modelBuilder.Entity<Utilisateurs>().ToTable("Utilisateur");

            modelBuilder.Entity<Competence>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Competence>()
                .HasOne(c => c.Candidat)
                .WithMany(ca => ca.Competence)
                .HasForeignKey(c => c.CandidatiId)  // propriété existante dans l’entité
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Formation>()
                    .HasKey(f => f.Id);

            modelBuilder.Entity<Formation>()
                .HasOne(f => f.Candidat)
                .WithMany(ca => ca.Formation)
                .HasForeignKey(f => f.CandidatiId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recruteur>()
                .HasOne(R => R.Entreprise)
                .WithMany(E => E.Recruteurs)
                .HasForeignKey(R => R.EntrepriseId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
