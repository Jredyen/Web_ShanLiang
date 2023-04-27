using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Reconnend()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from s in db.Stores select s;
            return View(datas);
        }
        public IActionResult Latest()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from s in db.Stores orderby s.StoreId descending select s;
            return View(datas);
        }
        public IActionResult Restaurant(int? id)
        {
            if (id == null)
                return RedirectToAction("Reconnend");
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from s in db.Stores 
                        where s.StoreId == id 
                        select s;
            if (datas == null)
                return RedirectToAction("Reconnend");
            return View(datas);
        }
    }
}
