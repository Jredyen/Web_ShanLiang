using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Linq;
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
            Admin admin = db.Admins.FirstOrDefault(a => a.AdminName == vm.AccountName && a.Passwoed == vm.AccountPassword);
            if (sto != null || mem != null || admin !=null)
            {
                string json;
                if (sto != null)
                {
                    json = JsonSerializer.Serialize(sto);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER_ROLE, "2");
                }

                else if (mem != null)
                {
                    json = JsonSerializer.Serialize(mem);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER_ROLE, "1");
                }
                else  {

                    json = JsonSerializer.Serialize(admin);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER_ROLE, "0");
                }

                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
                if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == "0")
                {
                    return RedirectToAction("Index", "Admin");
                }

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
        public IActionResult Mypage(/*Account x*//*這裡要帶入使用者身分*/)  //5/8併完後，註解掉
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == "1")
            {
                return RedirectToAction("memberManagement");
            }
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == "0")
            {
                return RedirectToAction("Index","Admin");
            }
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER_ROLE) == "2")
            {
                return RedirectToAction("memberManagement");
            }
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
        //public IActionResult Mypage()
        //{
        //    if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
        //        return RedirectToAction("memberManagement");
        //    return RedirectToAction("Login");
        //}

        public IActionResult memberManagement()
        {
            ShanLiang21Context sl = new ShanLiang21Context();
            //CAccountPasswordViewModel vm = new CAccountPasswordViewModel();
            string logginedUser = null;
            logginedUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member datas = JsonSerializer.Deserialize<Member>(logginedUser);
            var data = sl.Members.Where(t => t.Email.Contains(datas.Email));
            ViewBag.MemberName = datas.MemberName;
            return View(data);
        }

        public IActionResult memberDataRevision(string? Email)
        {

            ShanLiang21Context sl = new ShanLiang21Context();
            //CAccountPasswordViewModel vm = new CAccountPasswordViewModel();
            Member mem = sl.Members.FirstOrDefault(m => m.Email == Email);
            if (mem == null)
                return RedirectToAction("memberManagement");
            return View(mem);
            
            
        }
        
        public IActionResult memberDataRevision2(CMemberWrap m)
        {
            ShanLiang21Context sl = new ShanLiang21Context();
            Member mem = sl.Members.FirstOrDefault(p => p.Email == m.Email);
            if (mem != null) {
                if (m.Password != null)
                {
                    mem.Memberphone =  m.Memberphone;
                    mem.MemberName = m.MemberName;
                    mem.Address = m.Address;
                    mem.Password = m.Password;
                }
                
                if (m.Password == null) {
                    mem.Memberphone = m.Memberphone;
                    mem.MemberName = m.MemberName;
                    mem.Address = m.Address;
                }
            }


            sl.SaveChanges();

            return RedirectToAction("memberDataRevision");

        }
       
    }
}
