using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Services
{
    public class ExpertService : IExpertService
    {
        private readonly AppDbContext _context;


        public ExpertService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ExpertHeader>> GetHeader() => await _context.ExpertHeaders.ToListAsync();

       
    }


}
