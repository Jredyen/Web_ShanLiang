using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Collections.Generic;
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
        //public IActionResult Menu(int? StoreId)
        //傳入店家ID
        public IActionResult Menu()
        {
            //IEnumerable<MealMenu> datas = _db.MealMenus.Include(m => m.Store).Where(t => t.StoreId == StoreId);
            IEnumerable<MealMenu> datas = _db.MealMenus;
            return View(datas);
        }
        public IActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("Menu");
            ViewBag.MealId = id;
            return View();
        }

        [Route("api/shoppingcart/add-to-cart")]
        public ActionResult AddToCart(int? id, int? count)
        {   //增加到購物車
            MealMenu menu = _db.MealMenus.FirstOrDefault(t => t.MealId == id);
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
                        shoppingItem.count += (int)count;
                        verify = true;
                        break;
                    }
                }
                if (!verify)
                {
                    CShoppingCartItem item = new CShoppingCartItem();
                    item.mealmenu = menu;
                    item.price = (int)menu.MealPrice;
                    item.mealId = (int)id;
                    item.count = (int)count;
                    cart.Add(item);
                }
                //Todo假設沒有庫存回傳訊息
                //if (//沒庫存)
                //{ 
                //return Json(new { success = false ,message= "庫存不足" });
                //}
                json = JsonSerializer.Serialize(cart);  //購物車資料變回json
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_MENU_LIST, json);

            }
            return Json(new { success = true });
            //return RedirectToAction("Menu");
        }
        public IActionResult CartView()
        {  //檢視購物車
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_MENU_LIST))
                return RedirectToAction("Menu");
            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_MENU_LIST);

            if (json != "")
            {   //如果Session購物車沒東西
                List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                if (cart == null || cart.Count == 0)
                    return RedirectToAction("Menu");  //如果購物車是空的 回到Menu繼續點餐
                return View(cart);
            }
            return RedirectToAction("Menu");

        }

        public IActionResult Delete(int? id)
        {   //刪除購物車裡的餐點
            if (id == null)
                return RedirectToAction("Menu");

            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_MENU_LIST);
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if (cart == null)
            {
                cart = new List<CShoppingCartItem>();
            }
            for (int i = cart.Count - 1; i >= 0; i--)
            {
                if (cart[i].mealId == id)
                {
                    cart.RemoveAt(i);
                }
            }

            json = JsonSerializer.Serialize(cart);

            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_MENU_LIST, json);
            if (cart.Count == 0)
            {
                return RedirectToAction("Menu");
            }
            return RedirectToAction("CartView");
        }
        public IActionResult CheckoutCart()
        {   //確認訂單
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_MENU_LIST))
                return RedirectToAction("Menu");

            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_MENU_LIST);

            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);

            if (cart == null || cart.Count == 0)
                return RedirectToAction("Menu");  //如果購物車是空的 回到Menu繼續點餐


            //找出登入者資訊
            string logginedUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            //如果沒有登入轉跳登入頁面
            if (logginedUser == null)
            {

                return RedirectToAction("Login", "User");
            }
            Member datas = JsonSerializer.Deserialize<Member>(logginedUser);
            ViewBag.MemberName = datas.MemberName;
            ViewBag.MemberPhone = datas.Memberphone;

            return View(cart);
        }
        public IActionResult CreateOrder(int? sum)
        {   //付款後完成訂單
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_MENU_LIST))
                return RedirectToAction("Menu");//如果購物車沒東西 回點餐頁面

            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
                return RedirectToAction("Login", "User");//如果沒登入 回登入頁面

            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_MENU_LIST); //Session裡購物車物品轉字串

            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json); //字串轉購物車物件

            if (cart == null || cart.Count == 0)
                return RedirectToAction("Menu");  //如果購物車商品數量是0 回到Menu繼續點餐


            string jsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER); //Session裡會員資料轉字串

            Member datas = JsonSerializer.Deserialize<Member>(jsonUser);  //字串轉會員資料物件
            ViewBag.MemberName = datas.MemberName;
            ViewBag.MemberPhone = datas.Memberphone;
            //先寫進訂單資料表
            MealOrder mealOrder = new MealOrder();
            mealOrder.MemberId = datas.MemberId;
            mealOrder.StoreId = 1;  //寫死店家ID:1
            mealOrder.Total = sum;
            mealOrder.OrderStatus = 4; //訂單狀態:已完成
            mealOrder.OrderDate = DateTime.Now;
            _db.MealOrders.Add(mealOrder);
            _db.SaveChanges();
            //生成訂單後 再把購物車裡的物品一筆一筆放到訂單細節資料表
            foreach (var item in cart)
            {
                MealOrderDetail mealOrderDetail = new MealOrderDetail();
                mealOrderDetail.OrderId = mealOrder.OrderId;
                mealOrderDetail.MealId = item.mealId;
                mealOrderDetail.Quantity = item.count;
                _db.MealOrderDetails.Add(mealOrderDetail);
            }
            _db.SaveChanges();
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_MENU_LIST, ""); //清空購物車
            return View(cart);
        }
        public IActionResult MyMealOrder(int? id)
        {            //傳會員ID進來
            IEnumerable<MealOrder> datas = from s in _db.MealOrders.Include(m => m.Store).Include(m => m.OrderStatusNavigation)
                                           where s.MemberId == id
                                           select s;
            return View(datas);
        }
        public IActionResult MyMealOrderDetail(int? id)
        {
            IEnumerable<MealOrderDetail> datas = _db.MealOrderDetails.Include(m => m.Meal).Where(t => t.OrderId == id);
            return View(datas);
        }
    }
}
