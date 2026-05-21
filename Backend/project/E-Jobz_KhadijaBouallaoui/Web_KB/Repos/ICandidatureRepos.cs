using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Repos
{
    public interface ICandidatureRepos
    {
        //on ne met pas public car par defuatl sont public 
        Task<Candidature?> GetById(int id);
         Task AddCandidature(Candidature candidature);
        Task UpdateCandidature(Candidature candidature);
        Task DeleteCandidature(Candidature candidature);
        Task<List<Candidature>> GetByCandidatId(int candidatId);
        Task<List<Candidature>> GetByOffreId(int offreId);
        Task<List<Candidature>> GetByStatut(StatutCandidature statut);
        Task<bool> HasAlreadyAppliedAsync(int candidatId, int offreId);
        Task Save();
    }
}
