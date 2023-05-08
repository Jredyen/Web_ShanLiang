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
    }
}
