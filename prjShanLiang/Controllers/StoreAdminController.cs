using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class StoreAdminController : Controller
    {
        public IActionResult List()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from s in db.Stores
                        select s;

            return View(datas);
        }
    }
}
