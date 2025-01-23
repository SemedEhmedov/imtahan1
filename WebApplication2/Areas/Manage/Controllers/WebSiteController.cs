using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Areas.Manage.ViewModels.WebSite;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manage.Controllers
{
    public class WebSiteController : ManageBaseController
    {
        readonly AppDBContext _context;
        readonly IWebHostEnvironment _env;
        readonly IMapper _mapper;

        public WebSiteController(AppDBContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var websites = _context.WebSites.ToList();
            return View(websites);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateWebSiteVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var website = _mapper.Map<WebSite>(vm);
            _context.WebSites.Add(website);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var website = await _context.WebSites.FirstOrDefaultAsync(x => x.Id == id);
            if (website == null)
            {
                ModelState.AddModelError("", $"bu {id}-li website yoxdur ");
                return View();
            }
            _context.WebSites.Remove(website);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var oldwebsite = await _context.WebSites.FirstOrDefaultAsync(x => x.Id == id);
            if (oldwebsite == null)
            {
                ModelState.AddModelError("", $"bu {id}-li oldwebsite yoxdur ");
                return View();
            }
            UpdateWebSiteVm vm = new UpdateWebSiteVm()
            {
                Id = id,
                Name = oldwebsite.Name,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateWebSiteVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var oldwebsite = await _context.WebSites.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (oldwebsite == null)
            {
                ModelState.AddModelError("", $"bu {vm.Id}-li oldwebsite yoxdur ");
                return View();
            }
            _mapper.Map(vm, oldwebsite);
            _context.WebSites.Update(oldwebsite);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
