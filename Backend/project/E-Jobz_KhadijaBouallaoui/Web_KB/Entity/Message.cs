using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Message")]
    public class Message :Base
    {
        public string Contenu { get; set; }=string.Empty;
        public DateTime DateEnvoie { get; set; }
        // Expéditeur
        public int ExpediteurId { get; set; }  // clé étrangère
        public Utilisateurs? Expediteur { get; set; }

        // Destinataire
        public int DestinataireId { get; set; } // clé étrangère
        public Utilisateurs? Destinataire { get; set; }
    }
}
