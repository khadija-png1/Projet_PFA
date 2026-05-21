using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_KB.Shared;

namespace Web_KB.Entity
{
    [Table("Competence")]
    public class Competence :Base
    {
        [Required]
        public string Nom { get; set; } = string.Empty;
        [Range(1, 5, ErrorMessage = "Le niveau doit être entre 1 et 5.")]
        public int Niveau { get; set; }
        public int CandidatiId { get; set; }  // clé étrangère
        public Candidat? Candidat { get; set; }
    }
}
