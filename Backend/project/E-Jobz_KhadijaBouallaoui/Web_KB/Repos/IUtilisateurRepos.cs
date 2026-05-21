using Web_KB.Entity;
using Web_KB.Models;

namespace Web_KB.Repos
{
    public interface IUtilisateurRepos
    {
        Utilisateurs? GetByEmail(string email);
        void Add(Utilisateurs utilisateur);
        void SaveChanges();
        void Update(Utilisateurs user);
    }
}
