using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web_KB.Entity;

namespace Web_KB.Services
{
    public interface IRectruteurProfilService
    {
        Task<Recruteur?> RecruteurProfil(int id);
        Task<List<OffreEmploi>> GetOffresAvecCandidatures(int entrepriseId);
        Task UpdateRecruteur(Recruteur recruteur);

    }
}