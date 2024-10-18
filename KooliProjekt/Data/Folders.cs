namespace KooliProjekt.Data
{
    public class Folders
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;
    }
}
