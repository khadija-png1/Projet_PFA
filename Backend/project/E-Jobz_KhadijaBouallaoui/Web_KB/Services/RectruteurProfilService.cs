using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Web_KB.Entity;
using Web_KB.Repos;

namespace Web_KB.Services
{
    public class RectruteurProfilService:IRectruteurProfilService
    {
        private readonly IRecruteurRepos _repo;
        public RectruteurProfilService(IRecruteurRepos repo)
        {
            _repo = repo;
        }

        public async Task<Recruteur?> RecruteurProfil(int id)
        {
            return await _repo.GetById(id);
        }
        public async Task UpdateRecruteur(Recruteur recruteur)
        {
            await _repo.UpdateRecruteur(recruteur);
        }

        public async Task<List<OffreEmploi>> GetOffresAvecCandidatures(int entrepriseId)
        {
            return await _context.OffresEmploi
                .Include(o => o.Candidature)
                    .ThenInclude(c => c.Candidat)
                .Where(o => o.EntrepriseId == entrepriseId)
                .ToListAsync();
        }

    }
}
