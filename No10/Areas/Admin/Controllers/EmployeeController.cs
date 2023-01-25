using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No10.Context;
using No10.Dtos.Employee;
using No10.Helpers;
using No10.Models;
using System.Data;

namespace No10.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class EmployeeController : Controller
    {
        private readonly BusinessDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(BusinessDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Employee> emps = _context.Employees.Include(e=>e.Icons).ToList();
            if (emps == null)
            {
                return View();
            }
            List<EmployeeGetDto> getdtos = _mapper.Map < List < EmployeeGetDto >> (emps);
            return View(getdtos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                foreach (var item in postDto.Icons)
                {
                    if (item.IconName == null || item.IconUrl == null)
                    {
                        ModelState.AddModelError("Icons", "Field is required");
                    }
                }
                return View();
            }
            Employee emp = _mapper.Map<Employee>(postDto);
            string imagename = Guid.NewGuid() + postDto.formFile.FileName;
            string path=Path.Combine(_env.WebRootPath,"assets/img/team",imagename);
            using (FileStream fileStream = new FileStream(path,FileMode.Create))
            {
                postDto.formFile.CopyTo(fileStream);
            }
            emp.Image = imagename;
            foreach (var item in postDto.Icons)
            {
                emp.Icons.Add(item);
            }
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Employee emp = _context.Employees.Include(e=>e.Icons).Where(e=>e.Id==id).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            Helper.DeleteFile(_env.WebRootPath, emp.Image, "assets/img/team");
            _context.Employees.Remove(emp);
            _context.SaveChanges();
           return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Employee emp = _context.Employees.Include(e => e.Icons).Where(e => e.Id == id).FirstOrDefault();
            if (emp == null) 
            {
                return NotFound();
            }
            EmployeeGetDto getDto = _mapper.Map<EmployeeGetDto>(emp);
            EmployeeUpdateDto updateDto = new EmployeeUpdateDto { getDto = getDto };
            return View(updateDto) ;
        }
        [HttpPost]
        public IActionResult Update(EmployeeUpdateDto updateDto)
        {
            Employee emp = _context.Employees.Include(e => e.Icons).Where(e => e.Id == updateDto.getDto.Id).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            emp.Position = updateDto.PostDto.Position;
            emp.Name = updateDto.PostDto.Name;
            if (updateDto.PostDto.formFile != null)
            {
                string imagename = Guid.NewGuid() + updateDto.PostDto.formFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img/team", imagename);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    updateDto.PostDto.formFile.CopyTo(fileStream);
                }
                Helper.DeleteFile(_env.WebRootPath, emp.Image, "assets/img/team");
                emp.Image = imagename;
            }
            if(updateDto.PostDto.Icons != null)
            {
             for (int i = 0; i < updateDto.PostDto.Icons.Count; i++)
             {
                emp.Icons[i] = updateDto.PostDto.Icons[i];
             }
            }
            else { emp.Icons = null; }
            _context.Employees.Update(emp);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
