using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace prjShanLiang.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        [Route("api/Admin/AdminName")]
        public IActionResult AdminName()
        {
            string logginedAdmin = null;
            logginedAdmin = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Admin data = JsonSerializer.Deserialize<Admin>(logginedAdmin);

            return Json(new { success = true, adminName = data.AdminName });
        }

        public IActionResult List()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from a in db.Admins.Include(i => i.Identification)
                        select a;

            string logginedAdmin = null;
            logginedAdmin = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Admin data = JsonSerializer.Deserialize<Admin>(logginedAdmin);
            ViewBag.AdminName = data.AdminName;
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CCreateAdminAccountViewModel vm)
        {
            if (vm.IdentificationId == 3)
            {
                if (vm.AdminName != null && vm.Password != null && vm.Password2 != null)
                {
                    ShanLiang21Context db = new ShanLiang21Context();
                    if (!(db.Admins.Any(a => a.AdminName == vm.AdminName) || db.Stores.Any(s => s.AccountName == vm.AdminName)))
                    {
                        Admin admin = new Admin()
                        {
                            AdminName = vm.AdminName,
                            Passwoed = vm.Password,
                            IdentificationId = vm.IdentificationId
                        };
                        db.Add(admin);
                        db.SaveChanges();

                        return RedirectToAction("List");
                    }
                    else{
                        return View();
                    }
                }
                else {
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "無法修改管理員權限";
            }

            return View();
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
                ad.IdentificationId = a.IdentificationId;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult CheckAdminName(string name)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var storeExists = db.Stores.Any(s => s.AccountName == name);
            var adminExists = db.Admins.Any(a => a.AdminName == name);

            var exists = storeExists || adminExists;

            return Content(exists.ToString());
        }

        public IActionResult CheckAdminPassword(string password)
        {
            bool exists = false;
            if (password != null)
            {
                const string passwordPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$";
                exists = !Regex.IsMatch(password, passwordPattern);
            }

            return Content(exists.ToString());
        }
    }
}
