using Web_KB.Entity;

namespace Web_KB.Services
{
    public interface IChangeMotDePasseService
    {
        Utilisateurs? ChangeMotDePasse(string email, string motDePasse , string nouveauMotDePasse);
    }
}
