using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.Include(m => m.Images).Where(m => !m.SoftDelete).ToListAsync();
        
        public async Task<Product> GetById(int id) => await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
        
        public async Task<Product> GetFullDataById(int id) => await _context.Products.Include(m => m.Images).Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
        
    }
}
