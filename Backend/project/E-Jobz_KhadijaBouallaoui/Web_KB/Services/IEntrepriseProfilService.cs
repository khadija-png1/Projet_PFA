using Web_KB.Entity;

namespace Web_KB.Services
{
    public interface IEntrepriseProfilService
    {
        Task<Entreprise?> EntrepriseProfil(int id);
        Task AddEntreprise(Entreprise? entreprise);
        Task UpdateEntreprise(Entreprise entreprise);
    }
}
