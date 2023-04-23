using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
