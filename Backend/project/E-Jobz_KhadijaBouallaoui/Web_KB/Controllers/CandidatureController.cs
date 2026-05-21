using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Models;
using Web_KB.Services;
using Web_KB.Shared;

namespace Web_KB.Controllers
{
    public class CandidatureController: Controller
    {
        private readonly ILogger<CandidatureController> _logger;
        private readonly ICandidatureService _candidatureService;
        private readonly AppDbContext _context;
        public CandidatureController(
            ILogger<CandidatureController> logger,
            ICandidatureService candidatureService,
            AppDbContext context)
        {
            _logger = logger;
            _context = context;
            _candidatureService = candidatureService;
        }

      

        [HttpGet]
        public async Task<IActionResult> CandidatureCandidat()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");  // Utiliser "UserId" pour récupérer l'ID

            if (userid == null) return Unauthorized();
            var candidatures = await _candidatureService.GetByCandidatIdAsync(userid.Value);
            if (candidatures == null) return NotFound();

            var viewModel = candidatures.Select(c => new CandidatureViewModelCandidat
            {
                Id = c.Id,
                DateSoumission = c.DateSoumission,
                Statut = c.Statut,
                offre = c.OffreEmploi == null ? null : new OffresEmploiViewModelDetail
                {
                    Id = c.OffreEmploi.Id,
                    Titre = c.OffreEmploi.Titre,
                    Statut = c.OffreEmploi.Statut,
                    Entreprise = c.OffreEmploi.Entreprise == null ? null : new EntrepriseViewModel
                    {
                        Id = c.OffreEmploi.Entreprise.Id,
                        Nom = c.OffreEmploi.Entreprise.Nom,
                        
                       
                    }
                }
            }).OrderByDescending(c => c.DateSoumission)
                .ToList(); 


                

             return   View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CandidatureCandidat(PostulerViewModel model , EntrepriseProfilViewModel entreprisemodel)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var candidature = new Candidature
            {
                CandidatiId = userId.Value,
                OffreEmploiId = model.OffreEmploiId,
                DateSoumission = DateTime.Now,
                MessageMotivation=model.MessageMotivation,
                Statut = StatutCandidature.attente,


            };


            bool added = await _candidatureService.AddCandidature(candidature);

            if (!added)
            {
                TempData["Error"] = "Vous avez déjà postulé à cette offre.";
                return RedirectToAction("CandidatureCandidat", "Candidature", new { id = model.OffreEmploiId });
            }

            TempData["Success"] = "Candidature envoyée avec succès.";
            return RedirectToAction(nameof(CandidatureCandidat));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmerCandidature(int OffreEmploiId)
        {
            var offre = await _context.OffresEmploi
                .Where(o => o.Id == OffreEmploiId)
                .Select(o => new ConfirmerCandidatureViewModel
                {
                    OffreEmploiId = o.Id,
                    TitreOffre = o.Titre,
                    StatutOffre = o.Statut.ToString()
                })
                .FirstOrDefaultAsync();

            if (offre == null) return NotFound();

            return View(offre);
        }

        public IActionResult CandidatureEntreprise()
        {
            return View();
        }






    }
}
