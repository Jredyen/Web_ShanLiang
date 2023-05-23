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
            
           
            //string jsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER); //Session裡會員資料轉字串
            //Store datas = JsonSerializer.Deserialize<Store>(jsonUser);  //字串轉會員資料物件

            var stores = _db.Stores.ToList();
            var AdImages = _db.StoreAdImages.ToList();
            var model = new Tuple<List<Store>, List<StoreAdImage>>(stores, AdImages);
            return View(model);
            
           
        }

        public IActionResult AddAdvToCart(int? id)
        {
           
            if (id == null)
                return RedirectToAction("BuyAdv");
            ViewBag.StordID = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddAdvToCart(CAddAdvToCartViewModel vm)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == null)
            {
                // 未登入，到登入页面
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == "2")
            {


                StoreAdImage adv = _db.StoreAdImages.FirstOrDefault(t => t.StoreId == vm.txtStoreID);



            if (adv != null)
            {
                string json = "";
                List<CShoppingCartItem> cart = null;
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_Adv))
                {
                    json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_Adv);
                    cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                }
                else
                {
                    cart = new List<CShoppingCartItem>();

                }
                CShoppingCartItem item = new CShoppingCartItem();
                item.price = (int)adv.ADPrice;
                item.StoreID = vm.txtStoreID;
                item.count = vm.txtCount;
                item.storeAdImage = adv;
                cart.Add(item);
                json = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_Adv, json);

            }
            return RedirectToAction("BuyAdv");
            }
            return RedirectToAction("Login", "User");
        }
        public IActionResult Delete(int? id)
        {   //刪除購物車裡的餐點
            if (id == null)
                return RedirectToAction("BuyAdv");

            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_Adv);
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if (cart == null)
            {
                cart = new List<CShoppingCartItem>();
            }
            for (int i = cart.Count - 1; i >= 0; i--)
            {
                if (cart[i].StoreID == id)
                {
                    cart.RemoveAt(i);
                }
            }

            json = JsonSerializer.Serialize(cart);

            HttpContext.Session.SetString(CDictionary.SK_LOGINED_Adv, json);
            if (cart.Count == 0)
            {
                return RedirectToAction("BuyAdv");
            }
            return RedirectToAction("CartView");
        }


        public IActionResult CartView()
        {
            
            string json = "";
            List<CShoppingCartItem> cart = null;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_Adv))
            {
                json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_Adv);
                cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            }
            if (cart == null)
                return RedirectToAction("BuyAdv");
            return View(cart);
        }


    }
}
