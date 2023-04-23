using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.ViewModels
{
    public class SliderVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
