using Web_KB.Entity;
using Web_KB.Repositories.Interfaces;
using Web_KB.Shared;

namespace Web_KB.Services
{
    public class OffresEmploiService : IOffresEmploiService
    {
        private readonly IOffresEmploiRepos _repository;

        public OffresEmploiService(IOffresEmploiRepos repository)
        {
            _repository = repository;
        }

        public async Task<List<OffreEmploi>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OffreEmploi?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<OffreEmploi>> GetByEntrepriseAsync(int entrepriseId)
        {
            return await _repository.GetByEntrepriseAsync(entrepriseId);
        }

        public async Task CreateAsync(OffreEmploi offre)
        {
            offre.DatePublication = DateTime.Now;
            offre.Statut = StatutOffre.Active;

            await _repository.AddAsync(offre);
        }

        public async Task UpdateAsync(OffreEmploi offre)
        {
            await _repository.UpdateAsync(offre);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task ChangeStatutAsync(int id, StatutOffre statut)
        {
            var offre = await _repository.GetByIdAsync(id);
            if (offre == null) return;

            offre.Statut = statut;
            await _repository.UpdateAsync(offre);
        }
    }
}
