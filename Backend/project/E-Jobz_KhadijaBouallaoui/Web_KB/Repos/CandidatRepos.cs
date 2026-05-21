using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Web_KB.Data;
using Web_KB.Entity;

namespace Web_KB.Repos
{
    public class CandidatRepos : ICandidatRepos
    {
        
    
        private readonly AppDbContext _context;

        public CandidatRepos(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Candidat>> GetAll()
        {

            return await _context.Candidats
                    .AsNoTracking()
                    .Include(c => c.Competence)
                    .Include(c => c.Formation)
                    .ToListAsync();
        }
        public Candidat? GetById(int id)
        {
            return _context.Utilisateurs
                .OfType<Candidat>()
                .Include(c => c.Competence)
                .Include(c => c.Formation)
                .FirstOrDefault(c => c.Id == id);
        }


        public Candidat? GetByEmail(string email)
        => _context.Utilisateurs.OfType<Candidat>().FirstOrDefault(c => c.Email == email);
        public void Add(Candidat candidat)
        {
            _context.Utilisateurs.Add(candidat);
        }
        public void UpdateCandidat(Candidat candidat)
        {
            var existing = GetById(candidat.Id);
            if (existing == null) return;

            // Mettre à jour les champs simples
            existing.Nom = candidat.Nom;
            existing.Email = candidat.Email;
            existing.TitreProfil = candidat.TitreProfil;
            existing.Bio = candidat.Bio;
            existing.PhotoProfil = candidat.PhotoProfil;

            // --- Ajouter nouvelles compétences ---
            foreach (var c in candidat.Competence)
            {
                if (c.Id == 0 && !string.IsNullOrWhiteSpace(c.Nom)) // nouvelle compétence
                {
                    c.CandidatiId = candidat.Id;
                    _context.Competences.Add(c);
                }
                else
                {
                    // modification d'une compétence existante (si envoyée)
                    var existComp = existing.Competence.FirstOrDefault(x => x.Id == c.Id);
                    if (existComp != null)
                    {
                        existComp.Nom = c.Nom;
                        existComp.Niveau = c.Niveau;
                    }
                }
            }

            // --- Ajouter nouvelles formations ---
            foreach (var f in candidat.Formation)
            {
                if (f.Id == 0 && !string.IsNullOrWhiteSpace(f.Diplome)) // nouvelle formation
                {
                    f.CandidatiId = candidat.Id;
                    _context.Formations.Add(f);
                }
                else
                {
                    var existForm = existing.Formation.FirstOrDefault(x => x.Id == f.Id);
                    if (existForm != null)
                    {
                        existForm.Diplome =f.Diplome;
                        existForm.Ecole = f.Ecole;
                        existForm.DateDebut = f.DateDebut;
                        existForm.DateFin = f.DateFin;
                        existForm.Description = f.Description;
                    }
                }
            }
           

            Save();
        }

        public void UpdateCandidatComplet(Candidat candidat, List<Competence>? competences, List<Formation>? formations)
        {
            var existing = GetById(candidat.Id);
            if (existing == null) return;

            // Mettre à jour les champs simples
            existing.TitreProfil = candidat.TitreProfil;
            existing.Bio = candidat.Bio;
            existing.PhotoProfil = candidat.PhotoProfil; 


            // --- Mettre à jour les compétences ---
            if (competences != null)
            {
                foreach (var c in competences)
                {
                    if (c.Id == 0 && !string.IsNullOrWhiteSpace(c.Nom))
                    {
                        c.CandidatiId = candidat.Id;
                        _context.Competences.Add(c);
                    }
                    else
                    {
                        var existComp = existing.Competence.FirstOrDefault(x => x.Id == c.Id);
                        if (existComp != null)
                        {
                            existComp.Nom = c.Nom;
                            existComp.Niveau = c.Niveau;
                        }
                    }
                }
            }

            // --- Mettre à jour les formations ---
            if (formations != null)
            {
                foreach (var f in formations)
                {
                    if (f.Id == 0 && !string.IsNullOrWhiteSpace(f.Diplome))
                    {
                        f.CandidatiId = candidat.Id;
                        _context.Formations.Add(f);
                    }
                    else
                    {
                        var existForm = existing.Formation.FirstOrDefault(x => x.Id == f.Id);
                        if (existForm != null)
                        {
                            existForm.Diplome = f.Diplome;
                            existForm.Ecole = f.Ecole;
                            existForm.DateDebut = f.DateDebut;
                            existForm.DateFin = f.DateFin;
                            existForm.Description = f.Description ?? "";
                        }
                    }
                }
            }

            Save();
        }


        public void Save()
        {
            var n =_context.SaveChanges();
            

        }


    }
}


