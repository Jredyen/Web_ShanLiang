using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from a in db.Admins
                        select a;

            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Admin a)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            if (a.Identification != 0)
            {
                ViewBag.Message = "無法修改權限";
                return View();
            }

            db.Admins.Add(a);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Admin ad = db.Admins.FirstOrDefault(a => a.AdminId == id);
            if (ad != null)
            {
                db.Admins.Remove(ad);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Admin ad = db.Admins.FirstOrDefault(a => a.AdminId == id);
            if (ad == null)
            {
                return RedirectToAction("List");
            }
            return View(ad);
        }
        [HttpPost]
        public IActionResult Edit(Admin a)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Admin ad = db.Admins.FirstOrDefault(t => t.AdminId == a.AdminId);
            if (ad != null)
            {
                ad.AdminName = a.AdminName;
                ad.Passwoed = a.Passwoed;
                ad.Identification =a.Identification;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
