using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var datas = from m in db.Members.Include(s => s.AccountStatusNavigation)
                        select m;

            return View(datas);
        }
    }
}
