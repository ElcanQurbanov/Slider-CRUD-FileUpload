using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Models;
using Entity_Framework_Slider.Services.Interfaces;
using Entity_Framework_Slider.ViewModels;
using Newtonsoft.Json;

namespace Entity_Framework_Slider.Services
{
    public class BasketService : IBasketService
    {

        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddProductToBasket(BasketVM? existProduct, Product product, List<BasketVM> basket)
        {
            if (existProduct == null)
            {
                basket?.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1,

                });
            }
            else
            {
                existProduct.Count++;

            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public void DeleteProductFromBasket(int id)
        {
            List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            BasketVM deletedProduct = basketProducts.FirstOrDefault(m => m.Id == id);

            basketProducts.Remove(deletedProduct);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));
        }

        public List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }
    }
}
