using Microsoft.EntityFrameworkCore;
using Web_KB.Data;
using Web_KB.Entity;

namespace Web_KB.Repos
{
    public class RecruteurRepos: IRecruteurRepos
    {
        private readonly AppDbContext _context;
            public RecruteurRepos(AppDbContext context) { 
            _context = context;
        }

        public  async Task<Recruteur?> GetById(int id )
        {
            return await _context.Recruteurs
                .FirstOrDefaultAsync(c => c.Id == id);
        
        }
        public async Task<Recruteur?> GetByEmail(string email)
        {
            return await _context.Recruteurs
                .OfType<Recruteur>()
                .FirstOrDefaultAsync(c => c.Email == email);
        }
        public async Task UpdateRecruteur(Recruteur recruteur)
        {
             _context.Recruteurs.Update(recruteur);
             await _context.SaveChangesAsync();
        }

    }
}
