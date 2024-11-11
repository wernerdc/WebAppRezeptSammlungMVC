namespace WebAppRezeptSammlungMVC.Models
{
    public class Zutat
    {
        public int Id { get; set; }
        public int RezeptId { get; set; }
        public int LebensmittelId { get; set; }
        public int Menge {  get; set; }
        public string? Einheit { get; set; }

        // Navigation-Properties
        public Rezept? Rezept { get; set; }             // nullable, else it does not work
        public Lebensmittel? Lebensmittel { get; set; } // ^^
    }
}
