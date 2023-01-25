using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using No10.Dtos.UserDto;
using No10.Models;

namespace No10.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Index()
        //{
        //   await _roleManager.CreateAsync(new IdentityRole { Name="admin"});
        //   await _roleManager.CreateAsync(new IdentityRole { Name="user"});
        //    return Json("ok");
        //}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Register(RegisterDto registerDto )
        {
            AppUser exsist = await _usermanager.FindByNameAsync(registerDto.UserName);
            if(exsist != null)
            {
                ModelState.AddModelError("", "This username already exsist");
                return View(registerDto);
            }
            AppUser user = _mapper.Map<AppUser>(registerDto);
            var result=await _usermanager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(registerDto);
                }
            }
            await _usermanager.AddToRoleAsync(user, "user");
            return RedirectToAction("Index","Home");
        }
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
             var result=await _signInManager.PasswordSignInAsync(exsist,loginDto.Password,true,true);
            if (!result.Succeeded)
            {
               
                    ModelState.AddModelError("","Password or Username");
                
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
