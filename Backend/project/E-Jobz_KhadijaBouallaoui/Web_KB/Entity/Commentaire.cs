using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Commentaire")]
    public class Commentaire:Base
    {
        public string Contenu { get; set; } = string.Empty;
        public DateTime DateCommentaire { get; set; }
        public int PublicationId { get; set; }  // clé étrangère
        public Publication? Publication { get; set; }
        public int UtilisateursId { get; set; }  // clé étrangère
        public Utilisateurs? Utilisateurs { get; set; }
    }
}
