using System.ComponentModel.DataAnnotations;

namespace Web_KB.Models
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide")]
        public string Email { get; set; } = string.Empty;
    }
}
