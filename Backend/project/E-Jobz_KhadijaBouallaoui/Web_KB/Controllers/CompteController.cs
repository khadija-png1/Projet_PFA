using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Models;
using Web_KB.Services;
using Web_KB.Shared;

namespace Web_KB.Controllers
{
    public class CompteController : Controller
    {
        private readonly ILoginService _loginservice;
        private readonly IRegisterService _registerservice;
        private readonly IChangeMotDePasseService _changemotdepasseservice;
        private readonly ICandidatProfilService _candidatprofilservice;
        private readonly IEntrepriseProfilService _entrepriseProfilService;
        private readonly IRectruteurProfilService _rectruteurProfilService;
        private readonly ILogger<CompteController> _logger;
        private readonly AppDbContext _context;
        public CompteController(
             ILoginService loginservice,
             IRegisterService registerservice,
             IChangeMotDePasseService changemotdepasseservice,
             ICandidatProfilService candidatprofilservice,
             IEntrepriseProfilService entrepriseProfilService,
             IRectruteurProfilService rectruteurProfilService,
             ILogger<CompteController> logger,
             AppDbContext context

        )
        {
            _loginservice = loginservice;
            _registerservice = registerservice;
            _changemotdepasseservice = changemotdepasseservice;
            _candidatprofilservice = candidatprofilservice;
            _logger = logger;
            _context = context;
            _rectruteurProfilService = rectruteurProfilService;
            _entrepriseProfilService = entrepriseProfilService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string motDePasse)
        {
            var user = _loginservice.Login(email, motDePasse);
            if (user == null)
            {
                // Message d'erreur
                TempData["ErrorMessage"] = "Email ou Mot de passe incorrect.";
                return RedirectToAction("Login", "Compte"); // retourne à la page login
            }

            // Enregistrer l'ID de l'utilisateur dans la session
            HttpContext.Session.SetInt32("UserId", user.Id);

            // Enregistrer le rôle de l'utilisateur dans la session
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            // Passer l'ID de l'utilisateur et le rôle dans TempData["SuccessMessage"]
            TempData["SuccessMessage"] = $"Connexion réussie ! Bienvenue ";

            // Rediriger l'utilisateur vers son offre en fonction de son rôle
            if (user.Role == RoleEnum.Candidat)
            { TempData["SuccessMessage"] = $"Connexion réussie ! Bienvenue ";
                return RedirectToAction("OffresEmploisAll", "Offre");
            }
            else if (user.Role == RoleEnum.Recruteur)
            {
                return RedirectToAction("RecruteurProfil", "Compte");
            }

            // Si le rôle ne correspond à rien, retourner à la page de login par défaut
            return RedirectToAction("Login", "Compte");
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // retourne la vue avec les erreurs de validation (les <span asp-validation-for> montreront les messages)
                return View(model);
            }



            var user = _registerservice.Register(model.Nom, model.Prenom, model.Email, model.MotDePasse, model.Role);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Email existant ou erreur d'enregistrement.";
                return RedirectToAction("Register", "Compte");
            }

            TempData["SuccessMessage"] = "Inscription réussie !";
            return RedirectToAction("Login", "Compte");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Utilisateurs
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    return RedirectToAction("ChangeMotDePasse", "Compte", new { email = user.Email });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Aucun utilisateur trouvé avec cet email.");
                }
            }
            return View(model);
        }
        public IActionResult ChangeMotDePasse(string email)
        {

            return View();
        }

        [HttpPost]
        public IActionResult ChangeMotDePasse(string email, string motDePasse, string nouveauMotDePasse)
        {
            var user = _changemotdepasseservice.ChangeMotDePasse(email, motDePasse, nouveauMotDePasse);

            if (user != null) // Vérifie si le service a renvoyé null
            {

                if (user.Email == null)
                {
                    TempData["ErrorMessage"] = " Email  incorrect.";
                    return RedirectToAction("Index", "Home"); // retourne à la page login
                }
                if (user.MotDePasse == null)
                {
                    TempData["ErrorMessage"] = "Mot de passe incorrect.";
                    return RedirectToAction("Index", "Home"); // retourne à la page login
                }

            }
            TempData["SuccessMessage"] = "Mot de passe changé avec succès !";
            return RedirectToAction("Index", "Home");
        }

        // GET: Page de confirmation de déconnexion
        public IActionResult Logout()
        {
            
            // 02/01/2026 novalidate
            // Vérifier si connecté
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["ErrorMessage"] = "Vous n'êtes pas connecté.";
                return RedirectToAction("Login");
            }

            // Passer les données simples via ViewBag
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Email = HttpContext.Session.GetString("UserName");

            return View(); // ← Vue SANS ViewModel
        }

        // POST: Déconnexion effective
        [HttpPost]
        public IActionResult LogoutPost()
        {
            var userName = HttpContext.Session.GetString("UserName") ?? "Utilisateur";

            HttpContext.Session.Clear();
            Response.Cookies.Delete("UserEmail");

            TempData["SuccessMessage"] = $"Au revoir {userName} !";
            return RedirectToAction("Index", "Home");
        }



        [HttpGet("login-google")]
        public IActionResult LoginGoogle()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse") // Doit correspondre exactement à CallbackPath
            };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded || result.Principal == null)
                return RedirectToAction("Login", "Compte");

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;

            var utilisateur = _loginservice.LoginGoogle(email);

            if (utilisateur == null)
            {
                TempData["EmailInexistant"] = email;
                TempData["ErrorMessage"] = "EMAIL n'existe pas";
                return RedirectToAction("Register", "Compte");
            }

            // SignIn avec cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal);

            return RedirectToAction("Index", "Home");
        }

        // GET
        public IActionResult CandidatProfil()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");  // Utiliser "UserId" pour récupérer l'ID

            if (userid == null)
                return RedirectToAction("Error", "Error");


            var candidat = _candidatprofilservice.CandidatProfil(userid.Value);
            if (candidat == null) return NotFound();

            var viewModel = new CandidatProfilViewModel
            {
                Id = candidat.Id,
                Nom = candidat.Nom,
                Prenom = candidat.Prenom,
                Email = candidat.Email,
                TitreProfil = candidat.TitreProfil,
                Bio = candidat.Bio,
                PhotoProfil = candidat.PhotoProfil,
                Competence = candidat.Competence?.Any() == true ? candidat.Competence.ToList() : new List<Competence> { new Competence() },
                Formation = candidat.Formation?.Any() == true ? candidat.Formation.ToList() : new List<Formation> { new Formation { DateDebut = DateTime.Today, DateFin = DateTime.Today } }

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CandidatProfil(
                CandidatProfilViewModel model,
                string? addCompetence,
                string? addFormation,
                IFormFile? PhotoUpload)
        {
           // ✅ MAINTENANT SEULEMENT ModelState
            if (!ModelState.IsValid)
            {
                var candidatFromDb = _candidatprofilservice.CandidatProfil(model.Id);
                if (candidatFromDb != null)
                    model.PhotoProfil = candidatFromDb.PhotoProfil;

                return View(model);
            }

            // ✅ RÉCUPÉRER LE CANDIDAT
            var candidat = _candidatprofilservice.CandidatProfil(model.Id);
            if (candidat == null) return NotFound();

            candidat.TitreProfil = model.TitreProfil;
            candidat.Bio = model.Bio;

            // ✅ UPLOAD PHOTO
            if (PhotoUpload != null && PhotoUpload.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(PhotoUpload.FileName)}";
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "PhotoProfil",
                    fileName
                );

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using var stream = new FileStream(filePath, FileMode.Create);
                await PhotoUpload.CopyToAsync(stream);

                candidat.PhotoProfil = $"/PhotoProfil/{fileName}";
            }

            // --- FORMATIONS ---
            if (model.Formation != null && model.Formation.Any())
            {
                var newFormations = model.Formation
                    .Where(f => !string.IsNullOrWhiteSpace(f.Diplome))  // <- Diplome au lieu de NomFormation
                    .ToList();

                foreach (var f in newFormations)
                {
                    var existing = candidat.Formation.FirstOrDefault(x => x.Id == f.Id);
                    if (existing != null)
                    {
                        existing.Diplome = f.Diplome;      // <- mise à jour Diplome
                        existing.Ecole = f.Ecole;
                        existing.DateDebut = f.DateDebut;
                        existing.DateFin = f.DateFin;
                        existing.Description = f.Description;
                    }
                    else
                    {
                        candidat.Formation.Add(new Formation
                        {
                            Diplome = f.Diplome,
                            Ecole = f.Ecole,
                            DateDebut = f.DateDebut,
                            DateFin = f.DateFin,
                            Description = f.Description
                        });
                    }
                }

                // Supprimer doublons éventuels (basé sur Diplome)
                candidat.Formation = candidat.Formation
                    .GroupBy(x => x.Diplome.Trim().ToLower())
                    .Select(g => g.First())
                    .ToList();
            }
            // --- COMPETENCES ---
            if (model.Competence != null && model.Competence.Any())
            {
                // Supprimer les compétences vides
                var newCompetences = model.Competence
                    .Where(c => !string.IsNullOrWhiteSpace(c.Nom))
                    .ToList();

                foreach (var c in newCompetences)
                {
                    var existing = candidat.Competence.FirstOrDefault(x => x.Id == c.Id);
                    if (existing != null)
                    {
                        // Mise à jour
                        existing.Nom = c.Nom;
                        existing.Niveau = c.Niveau;
                    }
                    else
                    {
                        // Nouvelle compétence
                        candidat.Competence.Add(new Competence
                        {
                            Nom = c.Nom,
                            Niveau = c.Niveau,
                            CandidatiId = candidat.Id
                        });
                    }
                }

                // Supprimer doublons éventuels
                candidat.Competence = candidat.Competence
                    .GroupBy(x => x.Nom.Trim().ToLower())
                    .Select(g => g.First())
                    .ToList();
            }

            // Enfin, enregistrer en BD
            _candidatprofilservice.UpdateCandidatComplet(candidat, candidat.Competence, model.Formation);


            return RedirectToAction("CandidatProfil");
        }
        public IActionResult EditCompetences(int? candidatId)
        {
            if (candidatId == null) candidatId = HttpContext.Session.GetInt32("UserId");
            if (candidatId == null) return Unauthorized();

            var candidat = _candidatprofilservice.CandidatProfil(candidatId.Value);
            if (candidat == null) return NotFound();

            var viewModel = new CandidatProfilViewModel
            {
                Id = candidat.Id,
                Competence = candidat.Competence?.ToList() ?? new List<Competence>()
            };

            return View(viewModel);
        }
       
        [HttpPost]
        public IActionResult EditCompetences(CandidatProfilViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var candidat = _candidatprofilservice.CandidatProfil(model.Id);
            if (candidat == null) return NotFound();

            // On supprime toutes les compétences vides
            model.Competence = model.Competence
                .Where(c => !string.IsNullOrWhiteSpace(c.Nom))
                .ToList();

            // Mettre à jour les compétences existantes
            foreach (var c in model.Competence)
            {
                var existing = candidat.Competence.FirstOrDefault(x => x.Id == c.Id);
                if (existing != null)
                {
                    // Mise à jour
                    existing.Nom = c.Nom;
                    existing.Niveau = c.Niveau;
                }
                else
                {
                    // Ajout uniquement si c'est nouveau
                    candidat.Competence.Add(new Competence { Nom = c.Nom, Niveau = c.Niveau });
                }
            }

            // Supprimer les doublons éventuels
            candidat.Competence = candidat.Competence
                .GroupBy(x => x.Nom.Trim().ToLower())
                .Select(g => g.First())
                .ToList();

            _candidatprofilservice.UpdateCandidatComplet(candidat, candidat.Competence, null);

            return RedirectToAction("CandidatProfil");
        }







        // GET: Éditer les formations
        public IActionResult EditFormations(int? candidatId)
        {
            if (candidatId == null) candidatId = HttpContext.Session.GetInt32("UserId");
            if (candidatId == null) return Unauthorized();

            var candidat = _candidatprofilservice.CandidatProfil(candidatId.Value);
            if (candidat == null) return NotFound();

            var viewModel = new CandidatProfilViewModel
            {
                Id = candidat.Id,
                Formation = candidat.Formation?.ToList() ?? new List<Formation>()
            };

            return View(viewModel);
        }

        // POST: Éditer les formations

        [HttpPost]
        public IActionResult EditFormations(CandidatProfilViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var candidat = _candidatprofilservice.CandidatProfil(model.Id);
            if (candidat == null) return NotFound();

            // Supprimer les formations vides (Diplome obligatoire)
            model.Formation = model.Formation
                .Where(f => !string.IsNullOrWhiteSpace(f.Diplome))
                .ToList();

            foreach (var f in model.Formation)
            {
                var existing = candidat.Formation.FirstOrDefault(x => x.Id == f.Id);
                if (existing != null)
                {
                    // Mise à jour
                    existing.Diplome = f.Diplome;
                    existing.Ecole = f.Ecole;
                    existing.DateDebut = f.DateDebut;
                    existing.DateFin = f.DateFin;
                    existing.Description = f.Description;
                }
                else
                {
                    // Ajout nouveau
                    candidat.Formation.Add(new Formation
                    {
                        Diplome = f.Diplome,
                        Ecole = f.Ecole,
                        DateDebut = f.DateDebut,
                        DateFin = f.DateFin,
                        Description = f.Description,
                        CandidatiId = candidat.Id
                    });
                }
            }

            // Supprimer doublons éventuels (basé sur Diplome)
            candidat.Formation = candidat.Formation
                .GroupBy(x => x.Diplome.Trim().ToLower())
                .Select(g => g.First())
                .ToList();
            _candidatprofilservice.UpdateCandidatComplet(candidat, null, candidat.Formation);

            return RedirectToAction("CandidatProfil");
        }




        //----PROFIL POUR RECRUTEUR -----
        // GET
        public async Task<IActionResult> RecruteurProfil()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");  // Utiliser "UserId" pour récupérer l'ID

            if (userid == null)
                return RedirectToAction("Error", "Error");


            var recruteur = await  _rectruteurProfilService.RecruteurProfil(userid.Value);
            if (recruteur == null) return NotFound();

            var viewModel = new RecruteurProfilViewModel
            {
                Id = recruteur.Id,
                Nom = recruteur.Nom,
                Prenom = recruteur.Prenom,
                Email = recruteur.Email,
                PhotoProfil = recruteur.PhotoProfil,


            };

            return View(viewModel);
        }


        //-----LE POST DU PROFIL RECRUTEUR ----
        [HttpPost]
        public async Task<IActionResult> RecruteurProfil(RecruteurProfilViewModel model,IFormFile? PhotoUpload)
        {
            if (!ModelState.IsValid)
            {
                var recruteurFromDb = await _rectruteurProfilService.RecruteurProfil(model.Id);
                if (recruteurFromDb != null)
                    model.PhotoProfil = recruteurFromDb.PhotoProfil;

                return View(model);
            }
          
            // -Ona rien recupere sauf le nom et prenom email si il veux les change  -----
            var recruteur =  await _rectruteurProfilService.RecruteurProfil(model.Id);
            if (recruteur == null) return NotFound();

            recruteur.Nom = model.Nom;
            recruteur.Prenom = model.Prenom;

            // ✅ UPLOAD PHOTO
            if (PhotoUpload != null && PhotoUpload.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(PhotoUpload.FileName)}";
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "PhotoProfil",
                    fileName
                );

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using var stream = new FileStream(filePath, FileMode.Create);
                await PhotoUpload.CopyToAsync(stream);

                recruteur.PhotoProfil = $"/PhotoProfil/{fileName}";
            }


            // Enfin, enregistrer en BD
           await _rectruteurProfilService.UpdateRecruteur(recruteur);

            
            return RedirectToAction("RecruteurProfil","Compte");
        }














        //-------------------- ENTREPRISE
        [HttpGet]
        public async Task<IActionResult> EntrepriseProfil()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Compte");

            var model = new EntrepriseProfilViewModel(); 

            var recruteur = await _rectruteurProfilService.RecruteurProfil(userId.Value);
            if (recruteur == null)
            {
                TempData["Erreur"] = "Recruteur introuvable.";
                return View(model);
            }

            if (recruteur.EntrepriseId == null)
            {
                TempData["Erreur"] = "Aucune entreprise associée.";
                return View(model);
            }

            var entreprise = await _entrepriseProfilService.EntrepriseProfil(recruteur.EntrepriseId.Value);
            if (entreprise == null)
            {
                TempData["Erreur"] = "Entreprise introuvable.";
                return View(model);
            }

            model.Id = entreprise.Id;
            model.Nom = entreprise.Nom;
            model.Secteur = entreprise.Secteur;
            model.Description = entreprise.Description;
            model.Logo = entreprise.Logo;

            return View(model);
        }

        //-----------------ENTREPRISE POST -----------------------
        [HttpPost]
        public async Task<IActionResult> EntrepriseProfil(
                                             EntrepriseProfilViewModel model,
                                             IFormFile? LogoUpload)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Compte");

            var recruteur = await _rectruteurProfilService.RecruteurProfil(userId.Value);
            if (recruteur == null)
            {
                TempData["Erreur"] = "Recruteur introuvable.";
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);

          
            if (recruteur.EntrepriseId == null)
            {
                var entreprise = new Entreprise
                {
                    Nom = model.Nom,
                    Secteur = model.Secteur,
                    Description = model.Description
                };

                // Upload logo
                if (LogoUpload != null && LogoUpload.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(LogoUpload.FileName)}";
                    var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "PhotoProfil",
                        fileName
                    );

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await LogoUpload.CopyToAsync(stream);

                    entreprise.Logo = $"/PhotoProfil/{fileName}";
                }

               
                await _entrepriseProfilService.AddEntreprise(entreprise);

             
                recruteur.EntrepriseId = entreprise.Id;
                await _rectruteurProfilService.UpdateRecruteur(recruteur);

                return RedirectToAction("EntrepriseProfil");
            }

            var entrepriseExistante = await _entrepriseProfilService
                .EntrepriseProfil(recruteur.EntrepriseId.Value);

            if (entrepriseExistante == null)
            {
                TempData["Erreur"] = "Entreprise introuvable.";
                return View(model);
            }

            entrepriseExistante.Nom = model.Nom;
            entrepriseExistante.Secteur = model.Secteur;
            entrepriseExistante.Description = model.Description;

            if (LogoUpload != null && LogoUpload.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(LogoUpload.FileName)}";
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "PhotoProfil",
                    fileName
                );

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using var stream = new FileStream(filePath, FileMode.Create);
                await LogoUpload.CopyToAsync(stream);

                entrepriseExistante.Logo = $"/PhotoProfil/{fileName}";
            }

            await _entrepriseProfilService.UpdateEntreprise(entrepriseExistante);

            return RedirectToAction("RecruteurProfil");
        }





    }
}

