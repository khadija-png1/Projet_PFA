using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_KB.Entity;
using Web_KB.Models;
using Web_KB.Services;

namespace Web_KB.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        public async Task<IActionResult> AfficherPublication()
        {
            // Récupérer les publications de la base de données
            var publications = await _publicationService.GetAllPublicationsAsync();

            // Mapper les publications dans le modèle de vue
            var model = new PublicationPageViewModel
            {
                Publications = publications.Select(p => new PublicationViewModel
                {
                    Id = p.Id,
                    Contenu = p.Contenu,
                    Image = p.Image,
                    DatePublication = p.DatePublication,
                    UtilisateurNom = p.Utilisateurs != null ? $"{p.Utilisateurs.Prenom} {p.Utilisateurs.Nom}" : "Nom inconnu",
                    UtilisateurPhoto = p.Utilisateurs?.PhotoProfil ?? "/images/default-profile.png"
                }).ToList()
            };

            return View(model); // Retourne la vue avec les publications
        }

        // POST: Création d'une nouvelle publication
        [HttpPost]
        public async Task<IActionResult> CreatePublication(PublicationPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Le contenu de la publication est obligatoire.";
                return RedirectToAction(nameof(AfficherPublication)); // Corrigé pour revenir à AfficherPublication
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Error"] = "Vous devez être connecté pour publier.";
                return RedirectToAction("Error", "Error");
            }

            // Créer un objet publication à partir des données du modèle de vue
            var publication = new Publication
            {
                Contenu = model.NouvellePublication.Contenu,
                DatePublication = DateTime.Now,
                UtilisateursId = userId.Value // L'utilisateur connecté
            };

            // Vérifier si une image a été téléchargée
            if (model.NouvellePublication.Image != null)
            {
                // Récupérer le nom du fichier téléchargé
                var fileName = Path.GetFileName(model.NouvellePublication.Image.FileName);

                // Créer un chemin complet pour stocker l'image dans wwwroot/images
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                // Sauvegarder l'image dans le répertoire
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NouvellePublication.Image.CopyToAsync(stream); // Copie l'image dans le dossier
                }

                // Enregistrer le chemin relatif de l'image dans la base de données
                publication.Image = "/images/" + fileName;
            }

            // Ajouter la publication dans la base de données
            await _publicationService.AddPublicationAsync(publication);

            TempData["Success"] = "Publication ajoutée avec succès !";
            return RedirectToAction(nameof(AfficherPublication)); // Retourne à la page des publications après l'ajout
        }
    }
}
