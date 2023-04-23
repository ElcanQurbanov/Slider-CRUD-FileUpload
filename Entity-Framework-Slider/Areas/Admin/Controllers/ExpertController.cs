using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpertController : Controller
    {
        private readonly IExpertService _expertService;
        private readonly AppDbContext _context;

        public ExpertController(IExpertService expertService,
                                  AppDbContext context)
        {
            _expertService = expertService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _expertService.GetHeader());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertHeader expertHeader)
        {
            try
            {
                var existData = await _context.ExpertHeaders.FirstOrDefaultAsync(m => m.Title.Trim().ToLower() == expertHeader.Title.Trim().ToLower());
                if (existData != null)
                {
                    ModelState.AddModelError("Name", "This data already exist");
                    return View();
                }

                //int num = 1;
                //int num2 = 0;
                //int result = num / num2;

                await _context.ExpertHeaders.AddAsync(expertHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //ViewBag.error = ex.Message;
                return RedirectToAction("Error", new { msj = ex.Message });
            }

        }
        public IActionResult Error(string msj)
        {
            ViewBag.error = msj;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            ExpertHeader expertHeader = await _context.ExpertHeaders.FindAsync(id);

            if (expertHeader is null) return NotFound();

            _context.ExpertHeaders.Remove(expertHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id == null) return BadRequest();

            ExpertHeader expertHeader = await _context.ExpertHeaders.FindAsync(id);

            if (expertHeader is null) return NotFound();

            expertHeader.SoftDelete = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            ExpertHeader expertHeader = await _context.ExpertHeaders.FindAsync(id);

            if (expertHeader is null) return NotFound();

            return View(expertHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ExpertHeader expertHeader)
        {
            if (id == null) return BadRequest();

            if (ModelState.IsValid)
            {
                return View();
            }

            ExpertHeader dbExpertHeader = await _context.ExpertHeaders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbExpertHeader is null) return NotFound();

            if (dbExpertHeader.Title.Trim().ToLower() == expertHeader.Title.Trim().ToLower())
            {
                return RedirectToAction(nameof(Index));
            }

            //dbCategory.Name = category.Name;

            _context.ExpertHeaders.Update(expertHeader);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            ExpertHeader expertHeader = await _context.ExpertHeaders.FindAsync(id);

            if (expertHeader is null) return NotFound();

            return View(expertHeader);
        }
    }
}
