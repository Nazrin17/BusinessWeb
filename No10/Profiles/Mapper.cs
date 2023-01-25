
using AutoMapper;
using No10.Dtos.Employee;
using No10.Dtos.UserDto;
using No10.Models;

namespace No10.Profiles
{
    public class Mapper:Profile
    {
  
        public Mapper()
        {
            CreateMap<EmployeePostDto, Employee>();
            CreateMap<Employee, EmployeeGetDto>();
            CreateMap< RegisterDto, AppUser>();
            CreateMap<LoginDto,AppUser>();
        }
    }
}
