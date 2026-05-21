namespace Web_KB.Models
{
    public class PostulerViewModel
    {
        public int OffreEmploiId { get; set; }
      
        
        public string? MessageMotivation { get; set; }
        public string? Description { get; set; }
        public List<CandidatureViewModel> Candidatures { get; set; } = new();


    }
}
