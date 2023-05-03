using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

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
            Store sto = db.Stores.FirstOrDefault(a => a.AccountName == vm.AccountName && a.Password == vm.AccountPassword);
            Member mem = db.Members.FirstOrDefault(a => a.Email == vm.AccountName && a.Password == vm.AccountPassword);
            //Account acc = db.Accounts.FirstOrDefault(a => a.AccountName == vm.AccountName && a.AccountPassword == vm.AccountPassword);
            if (sto != null || mem != null)
            {
                string json;
                if (sto != null)
                    json = JsonSerializer.Serialize(sto);
                else
                    json = JsonSerializer.Serialize(mem);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
                return RedirectToAction("Index", "Home");
            }
                return View("Login");
        }
        public IActionResult MemberManager(string? account)
        {
            if (account == null /*|| acc == null*/)
                return RedirectToAction("Index", "Home");
            ShanLiang21Context db = new ShanLiang21Context();
            Member mem = db.Members.FirstOrDefault(m => m.AccountName == account);
            if (mem == null)
                return RedirectToAction("Index", "Home");
            return View(mem);
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
        public IActionResult Edit(string? account)
        {
            if (account == null)
                return View("Index", "Home");
            ShanLiang21Context db = new ShanLiang21Context();
            Account acc = db.Accounts.FirstOrDefault(a => a.AccountName == account);
            return View(acc);
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(CCreateMemberAccountViewModel vm)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Member mem = new Member()
            {
                AccountName = vm.AccountName,
                Memberphone = vm.Memberphone,
                MemberName = vm.MemberName,
                Email = vm.Email,
                BrithDate = vm.BrithDate,
                Address = vm.Address,
                CustomerLevel = 0,
                Password = vm.AccountPassword
            };
            //Account acc = new Account()
            //{
            //    AccountName = vm.AccountName,
            //    AccountPassword = vm.AccountPassword,
            //    Identification = 1
            //};
            db.Add(mem);
            //db.Add(acc);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult SignupStore()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignupStore(CCreateStoreAccountViewModel vm)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Store sto = new Store()
            {
                AccountName = vm.AccountName,
                TaxId = vm.TaxID,
                RestaurantName = vm.RestaurantName,
                RestaurantAddress = vm.RestaurantAddress,
                RestaurantPhone = vm.RestaurantPhone,
                DistrictId = vm.DistrictId,
                Seats = vm.Seats,
                StoreMail = vm.StoreMail,
                Password = vm.AccountPassword
            };
            //Account acc = new Account()
            //{
            //    AccountName = vm.AccountName,
            //    AccountPassword = vm.AccountPassword,
            //    Identification = 2
            //};
            db.Add(sto);
            //db.Add(acc);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Mypage()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
                return View();
            return RedirectToAction("Login");
        }
    }
}
