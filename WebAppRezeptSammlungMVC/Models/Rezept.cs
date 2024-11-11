namespace WebAppRezeptSammlungMVC.Models
{
    public class Rezept
    {
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public string Bezeichnung { get; set; }
        public string Zubereitung { get; set; }
        public string Zubereitungszeit {  get; set; }

        // Navigation
        public List<Zutat> Zutaten { get; set; } = new List<Zutat>();
    }
}
