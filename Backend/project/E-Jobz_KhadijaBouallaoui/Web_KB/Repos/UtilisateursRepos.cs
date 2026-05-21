using Microsoft.EntityFrameworkCore;
using System.Data;
using Web_KB.Data;
using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Repos
{
    public class UtilisateursRepos : IUtilisateurRepos
    {
        private readonly AppDbContext _context;

        public UtilisateursRepos(AppDbContext context)
        {
            _context = context;
        }

        public Utilisateurs? GetByEmail(string email) => _context.Utilisateurs.FirstOrDefault(u => u.Email == email);


        public void Add(Utilisateurs utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
        }
        

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Utilisateurs user)
        {
            _context.SaveChanges();
        }

    }
}
