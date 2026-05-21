using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_KB.Entity
{
    [Table("Base")]
   
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int CreerPar { get; set; }
        public bool EstSupprime { get; set; }
        public DateTime? DateSupperission { get; set; }
        public int? SupperimerPar { get; set; }
    }

}
