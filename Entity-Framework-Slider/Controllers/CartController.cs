using Entity_Framework_Slider.Data;
using Entity_Framework_Slider.Services.Interfaces;
using Entity_Framework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Entity_Framework_Slider.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public CartController(AppDbContext context,
                              IBasketService basketService,
                              IProductService productService)
        {
            _context = context;
            _basketService = basketService;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {

            //int basketCount;

            //if (Request.Cookies["basket"] != null)
            //{
            //    basketCount = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]).Count;
            //}
            //else
            //{
            //    basketCount = 0;
            //}

            //ViewBag.Count = basketCount;


            List<BasketVM> basketProducts = _basketService.GetBasketDatas();

            List<BasketDetailVM> basketDetails = new();


            foreach (var product in basketProducts)
            {
                var dbProduct = await _productService.GetFullDataById(product.Id);
                basketDetails.Add(new BasketDetailVM
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,  
                    CategoryName = dbProduct.Category.Name,
                    //Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.Where(m => m.IsMain).FirstOrDefault().Image,
                    Count = product.Count,
                    Total = dbProduct.Price * product.Count
                });
            }

            return View(basketDetails);
        }

        //[ActionName("Delete")]
        public IActionResult DeleteProductFromBasket(int? id)
        {
            if(id is null) return BadRequest();
            _basketService.DeleteProductFromBasket((int)id);
            return Ok();
        }

        
        
    }
}
