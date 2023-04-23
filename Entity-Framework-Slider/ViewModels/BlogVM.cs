using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public BlogHeader BlogHeader { get; set; }
    }
}
