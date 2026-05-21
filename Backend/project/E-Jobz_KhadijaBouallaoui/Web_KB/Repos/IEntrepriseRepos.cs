using Web_KB.Entity;

namespace Web_KB.Repos
{
    public interface IEntrepriseRepos
    {
        Task<List<Entreprise>> GetAll();
        Task<Entreprise?> GetById(int id);
        Task AddEntreprise(Entreprise? entreprise);
        Task updateEntreprise(Entreprise? entreprise);
    }
}
