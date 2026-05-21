using NuGet.Protocol.Core.Types;
using Web_KB.Entity;
using Web_KB.Repos;

namespace Web_KB.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepos _publicationRepos;

        public PublicationService(IPublicationRepos publicationRepos)
        {
            _publicationRepos = publicationRepos;
        }

        public async Task<List<Publication>> GetAllPublicationsAsync()
        {

            var publications = await _publicationRepos.GetAllAsync();

            foreach (var publication in publications)
            {
                // Vérifiez si DateSupperission est NULL avant d'accéder
                if (publication.DateSupperission == null)
                {
                    // Utilisez une valeur par défaut ou ignorer l'accès
                    publication.DateSupperission = DateTime.MinValue;  // Exemple de valeur par défaut
                }

                // Vérifiez si SupprimerPar est NULL avant d'accéder
                if (publication.SupperimerPar == null)
                {
                    // Utilisez une valeur par défaut ou ignorer l'accès
                    publication.SupperimerPar = -1;  // Exemple d'ID par défaut
                }
            }

            return publications;
        }

        
        public async Task<Publication?> GetPublicationByIdAsync(int id)
        {
            return await _publicationRepos.GetByIdAsync(id);
        }

        public async Task AddPublicationAsync(Publication publication)
        {
            // Ajouter la date de publication si elle n'est pas définie
            if (publication.DatePublication == default)
            {
                publication.DatePublication = DateTime.Now;
                if(publication.DateCreation == default)
                {
                    publication.DateCreation = DateTime.Now;
                }
                

            }

            await _publicationRepos.AddAsync(publication);
            await _publicationRepos.SaveChangesAsync();
        }
    }
}
