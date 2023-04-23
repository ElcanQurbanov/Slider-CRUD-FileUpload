using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.Services.Interfaces
{
    public interface IBlogService
    {
       Task<IEnumerable<Blog>> GetAll();
        Task<BlogHeader> GetBlogHeader();
    }
}
