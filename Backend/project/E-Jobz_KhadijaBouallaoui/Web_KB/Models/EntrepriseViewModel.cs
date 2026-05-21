namespace Web_KB.Models
{
    public class EntrepriseViewModel
    {
        public int Id { get; set; }
        public string? Nom { get; set; } 
        public string Secteur { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Logo { get; set; } = string.Empty;

    }

}
