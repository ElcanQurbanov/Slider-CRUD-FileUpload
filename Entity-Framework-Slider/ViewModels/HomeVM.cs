using Entity_Framework_Slider.Models;

namespace Entity_Framework_Slider.ViewModels
{
    public class HomeVM
    {
        //public List<Slider> Sliders { get; set; }
        //public SliderInfo SliderInfo { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int MyProperty { get; set; }
        public About About { get; set; }
        public IEnumerable<Advantage> Advantages { get; set; }
        public IEnumerable<Instagram> Instagrams { get; set; }
        public IEnumerable<Say> Says { get; set; }
        public IEnumerable<ExpertHeader> ExpertHeaders { get; set; }

    }
}
