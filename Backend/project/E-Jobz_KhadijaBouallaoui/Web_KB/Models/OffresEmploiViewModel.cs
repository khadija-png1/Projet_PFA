using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Models
{
    public class OffresEmploiViewModelAll
    {
        public int Id { get; set; } // Id de l'offre

        public string Titre { get; set; } = string.Empty;
        public DateTime DatePublication { get; set; }
        public StatutOffre Statut { get; set; }          
        public int EntrepriseId { get; set; }  
        public EntrepriseViewModel? Entreprise { get; set; }

       
    }
    public class OffresEmploiViewModelDetail
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
 
        public string Description { get; set; } = string.Empty;
    
        public string? TypeContrat { get; set; }
        public string? Lieu { get; set; }
        public decimal Salaire { get; set; }
        public DateTime DatePublication { get; set; }
        public StatutOffre Statut { get; set; }          
        public EntrepriseViewModel? Entreprise { get; set; }


    }
    public class AjoutOffresEmploiViewModel
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? TypeContrat { get; set; }
        public string? Lieu { get; set; }
        public decimal Salaire { get; set; }
        public DateTime DatePublication { get; set; }
        public StatutOffre Statut { get; set; }
        public EntrepriseViewModel? Entreprise { get; set; }
        public List<OffreEmploi> OffresEntreprise { get; set; } = new();



    }
    public class OffreListPageViewModel
    {
        public List<OffresEmploiViewModelDetail> Offres { get; set; } = new();
        public AjoutOffresEmploiViewModel NouveauOffre { get; set; } = new();
    }

}
