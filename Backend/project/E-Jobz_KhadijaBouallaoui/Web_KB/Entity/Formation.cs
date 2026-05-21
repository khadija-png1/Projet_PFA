using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Formation")]
    public class Formation :Base
    {
        [Required]
        public string Diplome { get; set; } = string.Empty;
        public string Ecole { get; set; } = string.Empty;
     
        public DateTime DateDebut { get; set; }
    
        public DateTime DateFin { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CandidatiId { get; set; }  // clé étrangère
        public Candidat? Candidat { get; set; }
    }
}
