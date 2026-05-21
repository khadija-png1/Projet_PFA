using Web_KB.Entity;

namespace Web_KB.Repositories.Interfaces
{
    public interface IOffresEmploiRepos
    {
        Task<List<OffreEmploi>> GetAllAsync();
        Task<OffreEmploi?> GetByIdAsync(int id);
        Task<List<OffreEmploi>> GetByEntrepriseAsync(int entrepriseId);

        Task AddAsync(OffreEmploi offre);
        Task UpdateAsync(OffreEmploi offre);
        Task DeleteAsync(int id);
    }
}
