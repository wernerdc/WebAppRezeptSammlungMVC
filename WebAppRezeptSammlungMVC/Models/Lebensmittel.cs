namespace WebAppRezeptSammlungMVC.Models
{
    public class Lebensmittel
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public string? Kategorie {  get; set; }

        // Navigation
        public List<Zutat> Zutaten {  get; set; } = new List<Zutat>();
    }
}
