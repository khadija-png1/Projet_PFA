using Web_KB.Entity;

namespace Web_KB.Services
{
    public interface IVerifyEmailService
    {
        Utilisateurs? VerifyEmail(string email);
    }
}
