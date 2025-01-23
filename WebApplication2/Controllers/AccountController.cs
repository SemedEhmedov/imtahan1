using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;
using WebApplication2.Helpers.Enums;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        readonly AppDBContext _context;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDBContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new AppUser()
            {
                Name = vm.Name,
                Email = vm.Email,
                UserName = vm.UserName,
            };
            if (user == null)
            {
                return View();
            }
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(vm.EmailOrUserName) ?? await _userManager.FindByNameAsync(vm.EmailOrUserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Giris melumatlari sehvdir");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Banlisiniz");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Giris melumatlari sehvdir");
                return View();
            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            await _signInManager.SignInAsync(user, vm.RememberMe);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString(),
                });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
