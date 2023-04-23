using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Helpers;
using Entity_Framework_Slider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext context,
                              IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog is null) return NotFound();

            return View(blog);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var existData = await _context.Blogs.FirstOrDefaultAsync(m => m.Header.Trim().ToLower() == blog.Header.Trim().ToLower());
                if (existData != null)
                {
                    ModelState.AddModelError("Header", "This data already exist");
                    return View();
                }

                if (!blog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }

                if (!blog.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + "-" + blog.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await blog.Photo.CopyToAsync(stream);
                }

                blog.Image = fileName;
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw;
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
            try
            {
                if (id == null) return BadRequest();

                Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

                if (blog is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", blog.Image);

                FileHelper.DeleteFile(path);

                _context.Blogs.Remove(blog);

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

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog is null) return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Blog blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!blog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }

                if (!blog.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + "-" + blog.Photo.FileName;

                Blog dbBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

                if (dbBlog.Photo == blog.Photo)
                {
                    return RedirectToAction(nameof(Index));
                }

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await blog.Photo.CopyToAsync(stream);
                }

                blog.Image = fileName;
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();

                string dbPath = FileHelper.GetFilePath(_env.WebRootPath, "img", dbBlog.Image);
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
