using Web_KB.Entity;
using Web_KB.Repos;

namespace Web_KB.Services
{
    public class ChangeMotDePasseService : IChangeMotDePasseService
    {
        private readonly IUtilisateurRepos _repo;

        public ChangeMotDePasseService(IUtilisateurRepos repo)
        {
            _repo = repo;
        }

        public Utilisateurs? ChangeMotDePasse(string email, string motDePasse, string nouveauMotDePasse)
        {
            Utilisateurs? user = _repo.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            // Vérification de l'ancien mot de passe
            if (user.MotDePasse != motDePasse)
            {
                return null;
            }

            // Mise à jour du mot de passe
            user.MotDePasse = nouveauMotDePasse;

            // Enregistrer la modification dans la base
            _repo.Update(user); // il faut que ton IUtilisateurRepos ait une méthode Update

            return user;
        }
    }
}
