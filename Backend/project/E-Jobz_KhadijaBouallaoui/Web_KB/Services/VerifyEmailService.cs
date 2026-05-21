using Web_KB.Entity;
using Web_KB.Repos;

namespace Web_KB.Services
{
    public class VerifyEmailService : IVerifyEmailService
    {
        private readonly IUtilisateurRepos _repo;

        public VerifyEmailService(IUtilisateurRepos repo)
        {
            _repo = repo;
        }
        public Utilisateurs? VerifyEmail(string email)
        {
            Utilisateurs? user = _repo.GetByEmail(email);

            if (user == null)
                return null;



            return user;
        }
    }
}
