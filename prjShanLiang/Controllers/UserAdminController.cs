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
            var datas = from m in db.Members.Include(s => s.AccountStatusNavigation).Include(e => e.StoreEvaluates)
                        select m;

            return View(datas);

            //var members = db.Members.Include(s => s.AccountStatusNavigation).ToList();
            //var storeEvaluate =db.StoreEvaluates.ToList();
            //var model = new Tuple<List<Member>, List<StoreEvaluate>>(members, storeEvaluate);
            //return View(model);
        }

        public IActionResult Delete(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Member mb = db.Members.FirstOrDefault(m => m.MemberId == id);
            if (mb != null)
            {
                // 刪除關聯資料
                var evaluations = db.StoreEvaluates.Where(e => e.MemberId == mb.MemberId);
                if(evaluations != null)
                db.StoreEvaluates.RemoveRange(evaluations);

                // 刪除主記錄
                db.Members.Remove(mb);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
