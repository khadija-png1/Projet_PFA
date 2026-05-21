using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Candidat")]
    public class Candidat :Utilisateurs
    {
     
        public string TitreProfil { get; set; } = string.Empty;
        public string?Bio { get; set; } 
        
        public List<Candidature> Candidature { get; set; } = new();
        public List<Formation> Formation { get; set; } = new();
        public List<Competence> Competence { get; set; } = new();


    }
}

  