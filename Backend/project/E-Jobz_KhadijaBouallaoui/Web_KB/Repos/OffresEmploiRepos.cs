using Microsoft.EntityFrameworkCore;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Repositories.Interfaces;

namespace Web_KB.Repositories
{
    public class OffresEmploiRepos : IOffresEmploiRepos
    {
        private readonly AppDbContext _context;

        public OffresEmploiRepos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OffreEmploi>> GetAllAsync()
        {
            return await _context.OffresEmploi
                .AsNoTracking()
                .Include(o => o.Entreprise)
                .ToListAsync();
        }

        public async Task<OffreEmploi?> GetByIdAsync(int id)
        {
            return await _context.OffresEmploi
                .Include(o => o.Entreprise)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<OffreEmploi>> GetByEntrepriseAsync(int entrepriseId)
        {
            return await _context.OffresEmploi
                .AsNoTracking()
                .Where(o => o.EntrepriseId == entrepriseId)
                .Include(o => o.Entreprise)
                .ToListAsync();
        }

        //asyn await pas dattent au niveau databa threads librer 
        public async Task AddAsync(OffreEmploi offre)
        {
            await _context.OffresEmploi.AddAsync(offre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OffreEmploi offre)
        {
            _context.OffresEmploi.Update(offre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var offre = await _context.OffresEmploi.FindAsync(id);
            if (offre == null) return;

            _context.OffresEmploi.Remove(offre);
            await _context.SaveChangesAsync();
        }
    }
}
