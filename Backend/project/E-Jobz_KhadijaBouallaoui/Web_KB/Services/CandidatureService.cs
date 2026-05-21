using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Repos;
using Web_KB.Shared;

namespace Web_KB.Services
{
    public class CandidatureService : ICandidatureService
    {
        private readonly ICandidatureRepos _repos;
        public CandidatureService(ICandidatureRepos repos)
        {
            _repos = repos;
        }

        // Récupérer par id
        public async Task<Candidature?> GetByIdAsync(int id)
        {
            return await _repos.GetById(id);
        }
        public async Task<bool> AddCandidature(Candidature candidature)
        {
            bool dejaPostule = await _repos.HasAlreadyAppliedAsync(
                candidature.CandidatiId,
                candidature.OffreEmploiId
            );

            if (dejaPostule)
            {
                return false; // déjà postulé
            }

            await _repos.AddCandidature(candidature);
            await _repos.Save();

            return true;
        }

        public async Task UpdateStatusCandidature(int candidatureId, StatutCandidature statut)
        {
            var candidature = await _repos.GetById(candidatureId);
            if (candidature == null) return;

            candidature.Statut = statut;
            await _repos.UpdateCandidature(candidature);


        }


        public async Task DeleteCandidature(int id)
        {
            var candidature = await _repos.GetById(id);
            if (candidature == null) return;

            await _repos.DeleteCandidature(candidature);
        }


        public async Task<List<Candidature>> GetByCandidatIdAsync(int candidatId)
        {
            return await _repos.GetByCandidatId(candidatId);
        }


        public async Task<List<Candidature>> GetByOffreIdAsync(int offreId)
        {
            return await _repos.GetByOffreId(offreId);
        }

        public async Task<List<Candidature>> GetByStatutAsync(StatutCandidature statut)
        {
            return await _repos.GetByStatut(statut);
        }
    }
}
