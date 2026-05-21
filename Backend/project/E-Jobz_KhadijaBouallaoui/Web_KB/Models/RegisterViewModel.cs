using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web_KB.Shared;

namespace Web_KB.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le Status  est obligatoire")]
        public RoleEnum Role { get; set; }

        [Required(ErrorMessage ="Le nom est obligatoire")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'e-mail est obligatoire")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, ErrorMessage = "Le {0} doit contenir au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = " mot de passe")]
        [Compare("ComfirmeMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string MotDePasse { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer  mot de passe ")]
        [Compare("ComfirmeMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]

        public string? ComfirmeMotDePasse { get; set; } 

    }
}
