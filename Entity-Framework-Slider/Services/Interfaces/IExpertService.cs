using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.Services.Interfaces
{
    public interface IExpertService
    {
        Task<IEnumerable<ExpertHeader>> GetHeader();
    }
}
