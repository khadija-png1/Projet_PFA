using System.ComponentModel.DataAnnotations.Schema;
using Web_KB.Shared;

namespace Web_KB.Entity
{
    [Table("Candidature")]

    public class Candidature :Base
    {
        public DateTime DateSoumission { get; set; }
        public StatutCandidature Statut { get; set; } 
        public string MessageMotivation { get; set; } = string.Empty;
        // Relation inversée
        public int OffreEmploiId { get; set; }  // clé étrangère
        public OffreEmploi? OffreEmploi { get; set; }
        public int CandidatiId { get; set; }  // clé étrangère
        public Candidat? Candidat { get; set; }
    }
}
