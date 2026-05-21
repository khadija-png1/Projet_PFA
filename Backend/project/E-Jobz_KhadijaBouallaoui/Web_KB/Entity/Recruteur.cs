using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity

    
{
    [Table("Recruteur")]
    public class Recruteur :Utilisateurs
    { 
        public int? EntrepriseId { get; set; }  // clé étrangère
        public Entreprise? Entreprise { get; set; }
    }
}
