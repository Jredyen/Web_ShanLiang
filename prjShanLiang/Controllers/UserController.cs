using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;

namespace prjShanLiang.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CAccountPasswordViewModel vm)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Account acc = db.Accounts.FirstOrDefault(a => a.AccountName == vm.AccountName && a.AccountPassword == vm.AccountPassword);
            if (acc == null)
                return View("Login");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult MemberManager(string? account)
        {
            if (account == null /*|| acc == null*/)
                return RedirectToAction("Index", "Home");
            ShanLiang21Context db = new ShanLiang21Context();
            Member mbr = db.Members.FirstOrDefault(m => m.AccountName == account);
            if (mbr == null)
                return RedirectToAction("Index", "Home");
            return View(mbr);
        }
        public IActionResult StoreManager(string? account)
        {
            if (account == null /*|| acc == null*/)
                return RedirectToAction("Index", "Home");
            ShanLiang21Context db = new ShanLiang21Context();
            Store sto = db.Stores.FirstOrDefault(s => s.AccountName == account);
            if (sto == null)
                return RedirectToAction("Index", "Home");
            return View(sto);
        }
        public IActionResult Mypage(Account x/*這裡要帶入使用者身分*/)
        {
            if (x.Identification == 0)
                return RedirectToAction("AmdinManager");
            if (x.Identification == 1)
                return RedirectToAction("MemberManager");
            if (x.Identification == 2)
                return RedirectToAction("StoreManager");
            else
                return RedirectToAction("Login");
        }
    }
}
