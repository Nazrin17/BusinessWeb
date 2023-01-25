namespace No10.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public List<Icon> Icons { get; set; }
    }
}
