using No10.Models;

namespace No10.Dtos.Employee
{
    public class EmployeePostDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public IFormFile formFile { get; set; }
        public List<Icon> Icons { get; set; }
    }
}
