using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_KB.Shared;

namespace Web_KB.Entity
{
    public class Utilisateurs  :Base
    {
       
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        public string Email { get; set; }=string.Empty;
        public string MotDePasse { get; set; }=string.Empty;
        public string? PhotoProfil { get; set; }
        public DateTime DateInscription { get; set; }

        public RoleEnum Role { get; set; }
        public List<Publication> Publication { get; set; } = new();
        public List<Commentaire> Commentaire { get; set; } = new();

        // Messages envoyés
        public List<Message>? MessagesEnvoyes { get; set; }

        // Messages reçus
        public List<Message>? MessagesRecus { get; set; }

    }
}