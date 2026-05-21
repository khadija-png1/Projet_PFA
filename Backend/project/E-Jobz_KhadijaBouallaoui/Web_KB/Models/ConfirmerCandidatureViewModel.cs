namespace Web_KB.Models
{
    public class ConfirmerCandidatureViewModel
    {
        public int OffreEmploiId { get; set; }
        public string TitreOffre { get; set; } = string.Empty;
        public string StatutOffre { get; set; } = string.Empty;
        public string MessageMotivation { get; set; } = string.Empty;
    }
}
