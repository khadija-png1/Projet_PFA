using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Entreprise")]
    public class Entreprise :Base
    {
        [Required]
        public string Nom { get; set; }= string.Empty;
        public string Secteur { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Logo { get; set; } = string.Empty;

        public List<Recruteur> Recruteurs { get; set; } = new();
        public List<OffreEmploi> OffreEmplois { get; set; } = new();

    }
}
