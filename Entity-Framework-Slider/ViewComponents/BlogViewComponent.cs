using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Entity_Framework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult(View(new BlogVM { Blogs = await _blogService.GetAll(),BlogHeader = await _blogService.GetBlogHeader()}));
        
    }
}
