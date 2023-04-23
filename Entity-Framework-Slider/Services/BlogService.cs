using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Blogs.Take(3).Where(m => !m.SoftDelete).ToListAsync();    
        }

        public async Task<BlogHeader> GetBlogHeader()
        {
            return await _context.BlogHeaders.FirstOrDefaultAsync();
        }
    }
}
