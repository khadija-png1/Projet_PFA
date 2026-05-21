using System.ComponentModel.DataAnnotations;

namespace Web_KB.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide")]
        public string Email { get; set; }=string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; } = string.Empty;
        public bool RemenbreMe { get; set; }
    }
}
