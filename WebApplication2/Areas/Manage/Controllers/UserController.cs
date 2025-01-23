using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Areas.Manage.ViewModels.User;
using WebApplication2.Context;
using WebApplication2.Helpers.Extensions;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manage.Controllers
{
    public class UserController : ManageBaseController
    {
        readonly AppDBContext _context;
        readonly IWebHostEnvironment _env;
        readonly IMapper _mapper;

        public UserController(AppDBContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        public IActionResult UserError()
        {
            return View();
        }
        public IActionResult Index()
        {
            var users = _context.Users.Include(x => x.WebSite).ToList();
            return View(users);
        }
        public IActionResult Create()
        {
            ViewBag.WebSites = _context.WebSites.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            ViewBag.WebSites = _context.WebSites.ToList();

            if (vm.Rating > 5)
            {
                ModelState.AddModelError("", "rating 5 den boyuk ola bilmez");
                return View();
            }

            if (!vm.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("", "faylin formati sehvdir");
                return View();
            }
            if (vm.File.Length > 2100000)
            {
                ModelState.AddModelError("", "faylin olcusu 2 mbdan cox ola bilmez");
                return View();
            }
            if (vm.File == null)
            {
                ModelState.AddModelError("", "doldurun");
                return View();
            }
            vm.ImgUrl = vm.File.Upload(_env.WebRootPath, "Upload/User");
            var user = _mapper.Map<User>(vm);
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var user = await _context.Users.Include(x => x.WebSite).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                ModelState.AddModelError("", $"bu {id}-li user yoxdur ");
                return View();
            }
            FileExtensions.DeleteFile(_env.WebRootPath, "Upload/User", user.ImgUrl);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                return View();
            }
            ViewBag.WebSites = _context.WebSites.ToList();
            var olduser = await _context.Users.Include(x => x.WebSite).FirstOrDefaultAsync(x => x.Id == id);
            if (olduser == null)
            {
                ModelState.AddModelError("", $"bu {id}-li user yoxdur ");
                return View();
            }
            UpdateUserVm vm = new UpdateUserVm()
            {
                Id = id,
                FullName = olduser.FullName,
                ImgUrl = olduser.ImgUrl,
                Descriptrion = olduser.Descriptrion,
                WebSiteId = olduser.WebSiteId,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserVm vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "doldurun");
                return RedirectToAction(nameof(UserError));
            }
            ViewBag.WebSites = _context.WebSites.ToList();
            var olduser = await _context.Users.Include(x => x.WebSite).FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (olduser == null)
            {
                ModelState.AddModelError("", $"bu {vm.Id}-li user yoxdur ");
                return View();
            }

            if (vm.Rating > 5)
            {
                ModelState.AddModelError("", "rating 5 den boyuk ola bilmez");
                return View();
            }
            if (!vm.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("", "faylin formati sehvdir");
                return View();
            }
            if (vm.File.Length > 2100000)
            {
                ModelState.AddModelError("", "faylin olcusu 2 mbdan cox ola bilmez");
                return View();
            }
            if (vm.File == null)
            {
                ModelState.AddModelError("", "doldurun");
                return View();
            }
            vm.ImgUrl = vm.File.Upload(_env.WebRootPath, "Upload/User");
            if (vm.ImgUrl == null)
            {
                ModelState.AddModelError("", "doldurun");
                return View();
            }
            _mapper.Map(vm, olduser);
            _context.Users.Update(olduser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
