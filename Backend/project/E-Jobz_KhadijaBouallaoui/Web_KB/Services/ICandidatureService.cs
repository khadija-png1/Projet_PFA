using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Services
{
    public interface ICandidatureService
    {

        Task <Candidature?> GetByIdAsync(int id);
        Task<bool> AddCandidature(Candidature candidature);
        Task UpdateStatusCandidature(int candidatureId, StatutCandidature statut);
        Task DeleteCandidature(int id);
        Task<List<Candidature>> GetByOffreIdAsync(int offreId);
        Task<List<Candidature>> GetByCandidatIdAsync(int candidatId);
        Task<List<Candidature>> GetByStatutAsync(StatutCandidature statut);
    }
}
