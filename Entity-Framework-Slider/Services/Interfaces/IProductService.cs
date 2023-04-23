using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetFullDataById(int id);
    }
}
