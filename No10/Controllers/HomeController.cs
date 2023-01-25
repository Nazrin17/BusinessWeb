using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No10.Context;
using No10.Dtos.Employee;
using No10.Models;
using System.Diagnostics;

namespace No10.Controllers
{
    public class HomeController : Controller
    {
        private readonly BusinessDbContext _context;
        private readonly IMapper _mapper;


        public HomeController(BusinessDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Employee> emps = _context.Employees.Include(e => e.Icons).ToList();
            if (emps == null)
            {
                return View();
            }
            List<EmployeeGetDto> getdtos = _mapper.Map<List<EmployeeGetDto>>(emps);
            return View(getdtos);
        }

    }
}