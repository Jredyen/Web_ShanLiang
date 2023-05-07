using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using System.Text.Json;

namespace prjShanLiang.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ShanLiang21Context _db;

        public ShoppingController(ShanLiang21Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RestaurantMenu()
        {
            IEnumerable<MealMenu> datas = _db.MealMenus;                       
            return View(datas);   
        }
        public IActionResult CartView()
        {
            //if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
            //    return RedirectToAction("RestaurantMenu");
            //string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);

            //RestaurantMenu<CShoppingCartItem> cart =
            //    JsonSerializer.Deserialize<RestaurantMenu<CShoppingCartItem>>(json);
            //if (cart == null)
            //    return RedirectToAction("RestaurantMenu");
            //return View(cart);
            return View();
        }
        public IActionResult CheckoutCart()
        {
            return View();
        }
    }
}
