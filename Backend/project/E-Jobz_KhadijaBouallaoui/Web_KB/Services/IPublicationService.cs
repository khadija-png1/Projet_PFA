using Web_KB.Entity;
namespace Web_KB.Services
{
    public interface IPublicationService
    {
        Task<List<Publication>> GetAllPublicationsAsync();
        Task<Publication?> GetPublicationByIdAsync(int id);
        Task AddPublicationAsync(Publication publication);
    }
}
