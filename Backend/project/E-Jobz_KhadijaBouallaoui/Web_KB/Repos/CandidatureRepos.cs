using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Repos
{
    public class CandidatureRepos: ICandidatureRepos
    {
        //on fait appel au dbcontext pour recupe note entite 
        private readonly AppDbContext _context;
        //on cree le constructeur de la classe 
        public CandidatureRepos(AppDbContext context)
        {
            _context = context;
        }
        //on fait appele a note method depuis interface 

      public  Task<Candidature?> GetById(int id)
        {

            return  _context.Candidatures
                .Include(c => c.OffreEmploi)
                  .ThenInclude(o => o.Entreprise)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddCandidature(Candidature candidature)
        {

            await _context.Candidatures.AddAsync(candidature);
           await _context.SaveChangesAsync();
        }
        public async Task UpdateCandidature(Candidature candidature)
        {
            _context.Candidatures.Update(candidature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidature(Candidature candidature)
        {
            _context.Candidatures.Remove(candidature);
           await Save();
        }
        public async Task<List<Candidature>> GetByCandidatId(int candidatId)
        {
            return await _context.Candidatures
        .Include(c => c.OffreEmploi) // ⚡️ charge l'entité OffreEmploi
        .Where(c => c.CandidatiId == candidatId)
        .ToListAsync();
        }
        // Liste par offre
        public async Task<List<Candidature>> GetByOffreId(int offreId)
        {
            return await _context.Candidatures
                .Where(c => c.OffreEmploiId == offreId)
                .ToListAsync();
        }
        // Liste par statut
        public async Task<List<Candidature>> GetByStatut(StatutCandidature statut)
        {
            return await _context.Candidatures
                .Where(c => c.Statut == statut)
                .ToListAsync();
        }
        // Vérifier si déjà postulé
        public async Task<bool> HasAlreadyAppliedAsync(int candidatId, int offreId)
        {
            return await _context.Candidatures
                .AnyAsync(c => c.CandidatiId == candidatId && c.OffreEmploiId == offreId);
        }
        public async Task Save() {
            await _context.SaveChangesAsync();
        }
    }
}
