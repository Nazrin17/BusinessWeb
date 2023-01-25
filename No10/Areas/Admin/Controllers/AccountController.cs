using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using No10.Dtos.UserDto;
using No10.Models;

namespace No10.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;


        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Index()
        //{
        //    AppUser user = new AppUser
        //    {
        //        UserName = "Admin",
        //    };
        //    var result = await _usermanager.CreateAsync(user, "Admin123@");

        //    if (!result.Succeeded)
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            return Json(item.Description);
        //        }
        //    }


        //    var result2 = await _usermanager.AddToRoleAsync(user, "admin");

        //    return Json("ok");
        //}

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser exsist = await _usermanager.FindByNameAsync(loginDto.UserName);
            if (exsist == null)
            {
                ModelState.AddModelError("", " username or password incorrect");
                return View(loginDto);
            }
            var result = await _signInManager.PasswordSignInAsync(exsist, loginDto.Password, true, true);
            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "Password or Username");

            }
            return RedirectToAction("Index", "Employee");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Employee");
        }
    }
}

