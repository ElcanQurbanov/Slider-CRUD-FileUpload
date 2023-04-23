using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<SliderInfo> GetSliderInfo()
        {
            return await _context.SliderInfos.FirstOrDefaultAsync();
        }
    }
}
