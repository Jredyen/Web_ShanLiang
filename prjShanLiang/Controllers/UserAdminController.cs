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
        }

        public IActionResult Edit(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Member mem = db.Members.FirstOrDefault(m => m.MemberId == id);
            if (mem == null)
            {
                return RedirectToAction("List");
            }
            return View(mem);
        }
        [HttpPost]
        public IActionResult Edit(Member m)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Member mem = db.Members.FirstOrDefault(mem => mem.MemberId == m.MemberId);
            if (mem != null)
            {
                mem.Email = m.Email;
                mem.MemberName = m.MemberName;
                mem.Memberphone = m.Memberphone;
                mem.BrithDate = m.BrithDate;
                mem.Address = m.Address;
                mem.AccountStatus = m.AccountStatus;

                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult CheckStatus(int? status)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var exists = db.AccountStatuses.Any(e => e.StatusId == status);
            return Content(exists.ToString());
        }
    }
}
