using System.ComponentModel.DataAnnotations;
using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Models
{
    public class CandidatureViewModelEntreprise
    {
        public int Id { get; set; }
        public DateTime DateSoumission { get; set; }
        public StatutCandidature Statut { get; set; }
        public string MessageMotivation { get; set; } = string.Empty;    
        public CandidatProfilViewModel? Candidat { get ; set; }
        public OffresEmploiViewModelDetail? offre { get; set; }
    }
    public class CandidatureViewModelCandidat
    {
        public int Id { get; set; }
        public DateTime DateSoumission { get; set; }
        [Required]
        public string? MessageMotivation { get; set; }
        public StatutCandidature Statut { get; set; }
        public OffresEmploiViewModelDetail? offre { get; set; }
    }
}
