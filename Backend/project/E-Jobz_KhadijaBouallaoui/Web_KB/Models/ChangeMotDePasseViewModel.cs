using System.ComponentModel.DataAnnotations;

namespace Web_KB.Models
{
    public class ChangeMotDePasseViewModel
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide")]
        public string? Email { get; set; }
        public string? AncienMotDePasse { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, ErrorMessage = "Le {0} doit contenir au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        [Compare("ComfirmeNouveauMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string? NouveauMotDePasse { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirmer nouveau mot de passe ")]
        public string? ComfirmeNouveauMotDePasse { get; set; } 
    }
}
