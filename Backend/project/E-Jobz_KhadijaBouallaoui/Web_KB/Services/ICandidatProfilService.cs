using Web_KB.Entity;

namespace Web_KB.Services
{
    public interface ICandidatProfilService
    {
        Candidat? CandidatProfil(int id);
        void UpdateCandidat(Candidat candidat);
        void UpdateCandidatComplet(Candidat candidat, List<Competence>? competences, List<Formation>? formations);

    }

}
