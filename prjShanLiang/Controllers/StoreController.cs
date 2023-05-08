using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class StoreController : Controller
    {
        private readonly ShanLiang21Context _db;
        public StoreController(ShanLiang21Context db)
        {
            _db = db;
        }

        public IActionResult List()
        {
            IQueryable<Store> datas = from s in _db.Stores orderby s.StoreId select s;
            return View(datas);
        }
        public IActionResult Reconnend()
        {
            IQueryable<Store> datas = from s in _db.Stores orderby s.Rating descending select s;
            return View(datas);
        }
        public IActionResult Latest()
        {
            IQueryable<Store> datas = from s in _db.Stores orderby s.StoreId descending select s;
            return View(datas);
        }
        public IActionResult Restaurant(int? id)
        {
            if (id == null)
                return RedirectToAction("Reconnend");
            IQueryable datas = from s in _db.Stores.Include(s => s.StoreDecorationImages)
                        where s.StoreId == id 
                        select s;
            if (datas == null)
                return RedirectToAction("Reconnend");
            return View(datas);
        }
        public IActionResult GetStore(string keyword)
        {
            IQueryable storeList = _db.Stores.Where(s => s.RestaurantName.Contains(keyword)).Select(s => s.RestaurantName);
            return Json(storeList);
        }
        public IActionResult ShowType()
        {
            IQueryable datas = _db.RestaurantTypes.Select(r => r.TypeName);
            return Json(datas);
        }
        public IActionResult SerachStore(string keyword, int[] type, int[] district)
        {
            IQueryable storeList = _db.Stores.Where(s => s.RestaurantName.Contains(keyword)).Select(s => new
            {
                //TODO:整合店名、類型與地區的搜尋
                //店名搜尋 : 完成
                //類型搜尋 : 未完成
                //地區搜尋 : 未完成
                s.RestaurantName,
                s.StoreId,
                s.Rating,
                s.OpeningTime,
                s.ClosingTime,
                s.RestaurantPhone,
                s.RestaurantAddress,
                imagePath = _db.StoreDecorationImages
                    .Where(x => x.StoreId == s.StoreId)
                    .Select(x => x.ImagePath).ToList(),
                typeName = _db.StoreTypes
                    .Where(st => st.StoreId == s.StoreId)
                    .Join(_db.RestaurantTypes,
            st => st.RestaurantTypeNum,
            rt => rt.RestaurantTypeNum,
            (st, rt) => rt.TypeName).ToList(),
            });
            return Json(storeList);
        }
    }
}
