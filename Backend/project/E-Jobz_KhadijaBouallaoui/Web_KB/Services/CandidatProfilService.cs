using Microsoft.EntityFrameworkCore;
using Web_KB.Entity;
using Web_KB.Repos;

namespace Web_KB.Services
{
    public class CandidatProfilService : ICandidatProfilService
    {
        private readonly ICandidatRepos _repo;

        public CandidatProfilService(ICandidatRepos repo)
        {
            _repo = repo;
        }
        public Candidat? CandidatProfil(int id)
        {
            return _repo.GetById(id);
        }
        // Mettre à jour le profil du candidat
        public void UpdateCandidat(Candidat candidat)
        {
            _repo.UpdateCandidat(candidat);
        }

        public void UpdateCandidatComplet(Candidat candidat, List<Competence>? competences, List<Formation>? formations)
        {
            _repo.UpdateCandidatComplet(candidat, competences, formations);
        }
    }

}
