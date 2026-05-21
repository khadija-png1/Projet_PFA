
using System.Text;
using Web_KB.Data; // Ton namespace où se trouve AppDbContext
using Web_KB.Entity;
using Web_KB.Repos;
using Web_KB.Shared;
using BCrypt.Net;
namespace Web_KB.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUtilisateurRepos _repo;


        public RegisterService(IUtilisateurRepos repo )
        {
            _repo = repo;
        }

        public Utilisateurs? Register(string nom, string prenom, string email, string motDePasse, RoleEnum role)
        {
            var   Email = _repo.GetByEmail(email);
            // Vérifier si l'email existe déjà
            if (Email != null)
                return null;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(motDePasse);
            Utilisateurs user;
            // 1. Crée l'utilisateur parent
            var utilisateur = new Utilisateurs
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                MotDePasse = hashedPassword,
                Role = role,
                DateInscription = DateTime.Now,
                DateCreation = DateTime.Now
            };
            _repo.Add(utilisateur);
            _repo.SaveChanges();

            if (role == RoleEnum.Candidat)
            {
              
                user = new Candidat
                {
                    Nom = nom,
                    Prenom = prenom,
                    Email = email,
                    MotDePasse = hashedPassword,
                    Role = RoleEnum.Candidat,
                    DateInscription = DateTime.Now,
                    DateCreation = DateTime.Now
                };
                
            }
            else if (role == RoleEnum.Recruteur)
            {
                
                user = new Recruteur
                {
                    Nom = nom,
                    Prenom = prenom,
                    Email = email,
                    MotDePasse = hashedPassword,
                    Role = RoleEnum.Recruteur,
                    DateInscription = DateTime.Now,
                    DateCreation = DateTime.Now
                };

               
            }
            else
            {
                return null;
            }
            _repo.Add(user);
            _repo.SaveChanges();

            return user;
        }



    }
}
