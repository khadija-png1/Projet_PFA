using System.ComponentModel.DataAnnotations;
using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Models
{
    public class CandidatProfilViewModel
    {
        public int Id { get; set; }

        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhotoProfil { get; set; }
        public string TitreProfil { get; set; } = string.Empty;
        public string? Bio { get; set; }
        
        public List<Formation> Formation { get; set; } = new();
        public List<Competence> Competence { get; set; } = new();
    }
    public class RecruteurProfilViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhotoProfil { get; set; }
        public EntrepriseViewModel? Entreprise { get; set; }


    }
    public class EntrepriseProfilViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Secteur { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Logo { get; set; } = string.Empty;



    }


}
