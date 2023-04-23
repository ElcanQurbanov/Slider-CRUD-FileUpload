using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.ViewModels;

namespace Entity_Framework_Slider.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetBasketDatas();
        void AddProductToBasket(BasketVM? existProduct, Product product, List<BasketVM> basket);
        void DeleteProductFromBasket(int id);
    }
}
