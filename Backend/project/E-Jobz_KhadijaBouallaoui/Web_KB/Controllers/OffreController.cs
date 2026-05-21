using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using Web_KB.Entity;
using Web_KB.Models;
using Web_KB.Services;
using Web_KB.Shared;

namespace Web_KB.Controllers
{
    public class OffreController : Controller
    {
        private readonly IOffresEmploiService _offresEmploiService;
        private readonly IEntrepriseProfilService _entrepriseProfilService;
        private readonly IRectruteurProfilService _rectruteurProfilService;
        public OffreController(
            IOffresEmploiService offresEmploiService,
            IEntrepriseProfilService entrepriseProfilService,
             IRectruteurProfilService rectruteurProfilService
             )
        {
            _offresEmploiService = offresEmploiService;
            _rectruteurProfilService = rectruteurProfilService;
            _entrepriseProfilService = entrepriseProfilService;
        }

        public async Task<IActionResult> OffresEmploisAll(string? titre, string? secteur)
        {
            var offres = await _offresEmploiService.GetAllAsync();

            //  Recherche par titre (ignore la casse)
            if (!string.IsNullOrWhiteSpace(titre))
            {
                offres = offres
                    .Where(o => o.Titre != null &&
                                o.Titre.ToLower().Contains(titre.ToLower()))
                    .ToList();
            }

            //  Recherche par secteur (ignore la casse)
            if (!string.IsNullOrWhiteSpace(secteur))
            {
                offres = offres
                    .Where(o => o.Entreprise != null &&
                                o.Entreprise.Secteur != null &&
                                o.Entreprise.Secteur.ToLower().Contains(secteur.ToLower()))
                    .ToList();
            }

            // 🔁 Mapping
            var model = offres.Select(o => new OffresEmploiViewModelAll
            {
                Id = o.Id,
                Titre = o.Titre,
                DatePublication = o.DatePublication,
                Statut = o.Statut,
                Entreprise = new EntrepriseViewModel
                {
                    Nom = o.Entreprise.Nom,
                    Secteur = o.Entreprise.Secteur
                }
            }).ToList();
            // 🔹 Passe les filtres à la vue pour remplir les champs du formulaire
            ViewData["TitreFilter"] = titre ?? "";
            ViewData["SecteurFilter"] = secteur ?? "";

            return View(model);
        }

        // DÉTAIL
        public async Task<IActionResult> OffresEmploiDetail(int id)
        {
            var offre = await _offresEmploiService.GetByIdAsync(id);
            if (offre == null) return NotFound();

            var model = new OffresEmploiViewModelDetail
            {
                Id = offre.Id,
                Titre = offre.Titre,
                Description = offre.Description,
                TypeContrat = offre.TypeContrat,
                Lieu = offre.Lieu,
                Salaire = offre.Salaire,
                DatePublication = offre.DatePublication,
                Statut = offre.Statut,
                Entreprise = new EntrepriseViewModel
                {
                    Nom = offre.Entreprise.Nom,
                    Description = offre.Entreprise.Description,
                   Logo = offre.Entreprise.Logo

                }
            };

            return View(model);
        }


        //------------------------AJOUT DES OFFRES DAPRE SLE RECTEUR/ENTREPRISE
        [HttpGet]
        public async Task<IActionResult> AjoutOffreEmploi()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Compte");

            var recruteur = await _rectruteurProfilService.RecruteurProfil(userId.Value);
            if (recruteur?.EntrepriseId == null)
                return RedirectToAction("EntrepriseProfil", "Compte");

            var entreprise = await _entrepriseProfilService
                .EntrepriseProfil(recruteur.EntrepriseId.Value);

            var offres = await _offresEmploiService
                .GetByEntrepriseAsync(entreprise.Id);

            var model = new AjoutOffresEmploiViewModel
            {
                Entreprise = new EntrepriseViewModel
                {
                    Id = entreprise.Id,
                    Nom = entreprise.Nom,
                    Logo = entreprise.Logo
                },
                DatePublication = DateTime.Now,

                OffresEntreprise = offres.ToList()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AjoutOffreEmploi(AjoutOffresEmploiViewModel model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Compte");

            var recruteur = await _rectruteurProfilService.RecruteurProfil(userId.Value);
            if (recruteur == null)
                return RedirectToAction("RecruteurProfil", "Compte");

            if (recruteur.EntrepriseId == null)
                return RedirectToAction("EntrepriseProfil", "Compte");

            if (!ModelState.IsValid)
            {
              
                var entreprise = await _entrepriseProfilService
                    .EntrepriseProfil(recruteur.EntrepriseId.Value);

                model.Entreprise = new EntrepriseViewModel
                {
                    Id = entreprise.Id,
                    Nom = entreprise.Nom,
                    Logo = entreprise.Logo
                };

                return View(model);
            }

            var offre = new OffreEmploi
            {
                Titre = model.Titre,
                Description = model.Description,
                TypeContrat = model.TypeContrat,
                Lieu = model.Lieu,
                Salaire = model.Salaire,
                DatePublication = DateTime.Now,
                Statut = StatutOffre.Active, 
                EntrepriseId = recruteur.EntrepriseId.Value 
            };

            await _offresEmploiService.CreateAsync(offre);

            return RedirectToAction("RecruteurProfil", "Compte");
        }

        [HttpGet]
        public async Task<IActionResult> OffrePostuler()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Compte");

            var recruteur = await _rectruteurProfilService.RecruteurProfil(userId.Value);
            if (recruteur?.EntrepriseId == null)
                return RedirectToAction("EntrepriseProfil", "Compte");

            var offres = await _offresEmploiService
                .GetOffresAvecCandidaturesAsync(recruteur.EntrepriseId.Value);

            var model = offres.Select(o => new PostulerViewModel
            {
                OffreId = o.Id,
                Titre = o.Titre,
                DatePublication = o.DatePublication,
                MessageMotivation = o.MessageMotivation,
                Candidatures = o.Candidature.Select(c => new CandidatureViewModelEntreprise
                {
                    DateCandidature = c.DateCandidature,
                    DateSoumission = c.DateSoumission,
                    MessageMotivation= c.MessageMotivation,
                    Statut = c.Statut
                }).ToList()
            }).ToList();

            return View(model);
        }
        

    }
}
