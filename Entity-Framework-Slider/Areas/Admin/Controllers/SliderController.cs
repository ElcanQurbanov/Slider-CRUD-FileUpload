using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Helpers;
using Entity_Framework_Slider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Areas.Admin.Controllers
{
    [Area("Admin")]
     public class SliderController : Controller
     {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m=>!m.SoftDelete).ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();
            return View(slider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }

                if (!slider.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + "-" + slider.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", fileName);

                using(FileStream stream = new FileStream(path, FileMode.Create))
                {
                   await slider.Photo.CopyToAsync(stream);
                }

                slider.Image = fileName;
                await _context.Sliders.AddAsync(slider);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

                if (slider is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", slider.Image);

                FileHelper.DeleteFile(path);

                _context.Sliders.Remove(slider);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }

                if (!slider.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + "-" + slider.Photo.FileName;

                Slider dbSlider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

                if (dbSlider.Photo==slider.Photo)
                {
                    return RedirectToAction(nameof(Index));
                }

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await slider.Photo.CopyToAsync(stream);
                }

                slider.Image = fileName;
                _context.Sliders.Update(slider);
                await _context.SaveChangesAsync();

                string dbPath = FileHelper.GetFilePath(_env.WebRootPath, "img", dbSlider.Image);
                FileHelper.DeleteFile(dbPath);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
