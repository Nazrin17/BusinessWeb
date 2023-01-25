namespace No10.Models
{
    public class Icon
    {
        public int Id { get; set; }
        public string IconName { get; set; }
        public string IconUrl { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
