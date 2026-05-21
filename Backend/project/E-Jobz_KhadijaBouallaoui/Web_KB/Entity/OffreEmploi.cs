using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_KB.Shared;

namespace Web_KB.Entity
{
    [Table("OffreEmploi")]
    public class OffreEmploi:Base
    {
        [Required]
        public string Titre { get; set; }= string.Empty;

        [Required]
        public string Description { get; set; }= string.Empty;
        [Required]
        public string? TypeContrat { get; set; }
        public string? Lieu { get; set; } 
        public decimal Salaire { get; set; }
        public  DateTime DatePublication { get; set; }
        public StatutOffre Statut { get; set; }          // Relation inversée
        public int EntrepriseId { get; set; }  // clé étrangère
        public Entreprise? Entreprise { get; set; }

        public List<Candidature> Candidature { get; set; } = new();
    }
}

