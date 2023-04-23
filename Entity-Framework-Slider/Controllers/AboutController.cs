using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework_Slider.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
