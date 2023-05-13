using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class UserAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from m in db.Members
                        select m;
            
            //var status  = db.Members.Where(m => m.)

            return View(datas);
        }
    }
}
