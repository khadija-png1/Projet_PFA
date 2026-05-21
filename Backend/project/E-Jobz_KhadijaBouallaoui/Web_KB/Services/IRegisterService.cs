using Web_KB.Entity;
using Web_KB.Shared;

namespace Web_KB.Services
{
    public interface IRegisterService
    {
        Utilisateurs? Register(string nom,string prenom ,string email, string motDePasse ,RoleEnum role );
    }
}
