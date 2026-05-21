using Microsoft.EntityFrameworkCore;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Repos;

public class PublicationRepos : IPublicationRepos
{
    private readonly AppDbContext _context;

    public PublicationRepos(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Publication>> GetAllAsync()
    {
        return await _context.Publications
            .Include(p => p.Utilisateurs)
            .OrderByDescending(p => p.DatePublication)
            .Select(p => new Publication
            {
                Id = p.Id,
                Contenu = p.Contenu,
                DatePublication = p.DatePublication,
                Image = p.Image,
                UtilisateursId = p.UtilisateursId,
                Utilisateurs = p.Utilisateurs,
                // Remplacer les valeurs NULL avec des valeurs par défaut
                DateSupperission = p.DateSupperission ?? DateTime.MinValue,
                SupperimerPar = p.SupperimerPar ?? -1,
                Commentaire = p.Commentaire
            })
            .ToListAsync();
    }


    public async Task<Publication?> GetByIdAsync(int id)
    {
        return await _context.Publications
                             .Include(p => p.Utilisateurs)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Publication publication)
    {
        await _context.Publications.AddAsync(publication);
        await _context.SaveChangesAsync();
    }
   
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
