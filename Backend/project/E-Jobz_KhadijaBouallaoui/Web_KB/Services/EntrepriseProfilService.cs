using Web_KB.Entity;
using Web_KB.Repos;

namespace Web_KB.Services
{
    public class EntrepriseProfilService: IEntrepriseProfilService
    {
        private readonly IEntrepriseRepos _repos;
        public EntrepriseProfilService(IEntrepriseRepos repos)
        {
            _repos = repos;
        }
        public async Task<Entreprise?> EntrepriseProfil(int id)
        {
            return await _repos.GetById(id);

        }
        public async Task AddEntreprise(Entreprise? entreprise)
        {
             await _repos.AddEntreprise(entreprise);
        }
        public async Task UpdateEntreprise(Entreprise entreprise)
        {
            await _repos.updateEntreprise(entreprise);

        }
    }
}
