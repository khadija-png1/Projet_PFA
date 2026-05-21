using Microsoft.EntityFrameworkCore;
using Web_KB.Data;
using Web_KB.Entity;

namespace Web_KB.Repos
{
    public class EntrepriseRepos: IEntrepriseRepos
    {
        private readonly AppDbContext _context;
        public EntrepriseRepos(AppDbContext context)
        {
            _context = context;
        }

       public async Task<List<Entreprise>> GetAll()
        {
            return await _context.Entreprises
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Entreprise?> GetById(int id)
        {
            return await _context.Entreprises
                .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task AddEntreprise(Entreprise? entreprise)
        {
           await _context.Entreprises.AddAsync(entreprise);
            await _context.SaveChangesAsync();
        }
        public async Task updateEntreprise(Entreprise? entreprise)
        {
            _context.Entreprises.Update(entreprise);
            await _context.SaveChangesAsync();


        }
    }
}
