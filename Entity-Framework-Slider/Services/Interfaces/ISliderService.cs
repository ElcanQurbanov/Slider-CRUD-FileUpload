using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAll();
        Task<SliderInfo> GetSliderInfo();
    }
}
