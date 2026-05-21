using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Publication")]
    public class Publication :Base 
    {
       
        public string Contenu { get; set; } = string.Empty;
        public DateTime DatePublication { get; set; }
        public string? Image { get; set; }
        public int UtilisateursId { get; set; }  // clé étrangère
        public Utilisateurs? Utilisateurs { get; set; }
        public List<Commentaire> Commentaire { get; set; } = new();

    }
}
