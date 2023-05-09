using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Text.Json;

namespace prjShanLiang.Controllers
{
    public class AdvertisementController : Controller
    {

        private readonly ShanLiang21Context _db;

        public AdvertisementController(ShanLiang21Context db)
        {

            _db = db;
        }
        public IActionResult BuyAdv()
        {


            var Image = _db.Stores.Take(2).ToList();
            var AdImages = _db.StoreAdImages.ToList();
            var model = new Tuple<List<Store>, List<StoreAdImage>>(Image, AdImages);
            return View(model);
           
        }
        public IActionResult AddToAdvCart(int? id)
        {
            if (id == null)
                return RedirectToAction("BuyAdv");
            ViewBag.MealId = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToAdvCart(CAddToCartViewModel vm)
        {
            MealMenu menu = _db.MealMenus.FirstOrDefault(t => t.MealId == vm.txtMealId);
            if (menu != null)
            {
                string json = "";
                List<CShoppingCartItem> cart = null;
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_MENU_LIST))
                { //如果比對有KEY就把json的值取出 cart還原回來
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_MENU_LIST);
                    cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                }
                else
                {
                    cart = new List<CShoppingCartItem>();
                }
                bool verify = false;
                foreach (var shoppingItem in cart)
                {
                    if (shoppingItem.mealId == menu.MealId)
                    {
                        shoppingItem.count += vm.txtCount;
                        verify = true;
                        break;
                    }
                }
                if (!verify)
                {
                    CShoppingCartItem item = new CShoppingCartItem();
                    item.mealmenu = menu;
                    item.price = (decimal)menu.MealPrice;
                    item.mealId = vm.txtMealId;
                    item.count = vm.txtCount;
                    cart.Add(item);
                }

                json = JsonSerializer.Serialize(cart);  //購物車資料變回json
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_MENU_LIST, json);
            }
            return RedirectToAction("BuyAdv");
        }
        public IActionResult CartView()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_MENU_LIST))
                return RedirectToAction("BuyAdv");
            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_MENU_LIST);

            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if (cart == null)
                return RedirectToAction("BuyAdv");
            return View(cart);
        }



    }
}
