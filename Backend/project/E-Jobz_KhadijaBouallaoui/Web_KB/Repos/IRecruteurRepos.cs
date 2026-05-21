using Web_KB.Entity;

namespace Web_KB.Repos
{
    public interface IRecruteurRepos
    {
        Task<Recruteur?> GetById(int id);
        Task<Recruteur?> GetByEmail(string email);
        Task UpdateRecruteur(Recruteur recruteur);


    }
}
