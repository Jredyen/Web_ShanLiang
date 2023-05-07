using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

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
        public IActionResult List()
        {
            IEnumerable<MealMenu> datas = null;
            
                datas = _db.MealMenus;
            
           
            return View(datas);   //強型別放到View
        }
    }
}
