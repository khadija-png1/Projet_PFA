using Web_KB.Entity;

namespace Web_KB.Repos
{
    public interface ICandidatRepos
    {
        Task<List<Candidat>> GetAll();
        Candidat? GetById(int id);
        Candidat? GetByEmail(string email);
        void Add(Candidat candidat);
        void UpdateCandidat(Candidat candidat);
        void UpdateCandidatComplet(Candidat candidat, List<Competence>? competences, List<Formation>? formations);
        void Save();
    }
}
