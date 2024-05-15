namespace VOSA_Events.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Event> Events { get; set; }
    }

    public class CategoryDto
    {
        public string Name { get; set; }
    }
}
