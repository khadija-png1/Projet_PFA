using Microsoft.CodeAnalysis.Scripting;
using Web_KB.Entity;
using Web_KB.Repos;
using BCrypt.Net;

namespace Web_KB.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUtilisateurRepos _repo;

        public LoginService(IUtilisateurRepos repo)
        {
            _repo = repo;
        }

        public Utilisateurs? Login(string email, string motDePasse)
        {
            // Récupérer l'utilisateur par email
            var user = _repo.GetByEmail(email);

            if (user == null)
                return null;

            // Vérifier le mot de passe avec BCrypt
            bool motDePasseCorrect = BCrypt.Net.BCrypt.Verify(motDePasse, user.MotDePasse);

            if (!motDePasseCorrect)

                return null;

            return user;
        }

        private string HashPassword(string motDePasse)
        {
            throw new NotImplementedException();
        }

        // Login via Google OAuth
        public Utilisateurs? LoginGoogle(string email)
        {
            var user = _repo.GetByEmail(email);

            // Si l'utilisateur n'existe pas, retourner null
            return user;
        }
    }
}

