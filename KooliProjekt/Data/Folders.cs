namespace KooliProjekt.Data
{
    public class Folders
    {
        // This is the primary key for the Folders table
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;

        // Lisame Done välja
        public bool? Done { get; set; }  // Kasutame nullable bool tüüpi
    }
}
