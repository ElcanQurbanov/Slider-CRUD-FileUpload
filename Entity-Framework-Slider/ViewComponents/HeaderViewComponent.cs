using Entity_Framework_Slider.Services.Interfaces;
using Entity_Framework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework_Slider.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(_layoutService.GetSettingDatas()));
        }
    }
}
