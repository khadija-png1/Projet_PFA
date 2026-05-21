using Web_KB.Entity;
namespace Web_KB.Repos
{
    public interface IPublicationRepos
    {
        Task<List<Publication>> GetAllAsync();
        Task<Publication?> GetByIdAsync(int id);
        Task AddAsync(Publication publication);
        Task SaveChangesAsync();
    }
}
