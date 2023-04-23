using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Entity_Framework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;

        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _sliderService.GetAll();
            var sliderInfo = await _sliderService.GetSliderInfo();
            var model = new SliderVM
            {
                Sliders = sliders,
                SliderInfo = sliderInfo
            };

            return await Task.FromResult(View(model));
        }
    }
}
