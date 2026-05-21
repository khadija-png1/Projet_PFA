using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_KB.Models
{
    // Représente une publication pour affichage
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = string.Empty;
        public string? Image { get; set; }
        public DateTime DatePublication { get; set; }
        public string UtilisateurNom { get; set; } = string.Empty;

        // ⚡ Photo de profil de l'utilisateur pour l'affichage
        public string? UtilisateurPhoto { get; set; }
    }

    // Pour le formulaire de création
    public class CreatePublicationViewModel
    {
        [Required(ErrorMessage = "Le contenu est obligatoire")]
        public string Contenu { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }

    public class PublicationPageViewModel
    {
        public List<PublicationViewModel> Publications { get; set; } = new();
        public CreatePublicationViewModel NouvellePublication { get; set; } = new();
    }
}
