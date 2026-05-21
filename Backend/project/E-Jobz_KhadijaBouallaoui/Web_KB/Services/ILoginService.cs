using Web_KB.Entity;
using Web_KB.Models;

namespace Web_KB.Services
{
    public interface ILoginService
    {
        Utilisateurs? Login(string email, string motDePasse);
        Utilisateurs? LoginGoogle(string email);


    }
}
