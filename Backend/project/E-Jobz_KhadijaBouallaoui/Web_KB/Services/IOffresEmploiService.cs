using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Services
{
    public interface IOffresEmploiService
    {
        Task<List<OffreEmploi>> GetAllAsync();
        Task<OffreEmploi?> GetByIdAsync(int id);
        Task<List<OffreEmploi>> GetByEntrepriseAsync(int entrepriseId);

        Task CreateAsync(OffreEmploi offre);
        Task UpdateAsync(OffreEmploi offre);
        Task DeleteAsync(int id);

        Task ChangeStatutAsync(int id, StatutOffre statut);
    }
}
