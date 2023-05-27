using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class StoreAdminController : Controller
    {
        private IWebHostEnvironment _enviro;
        private readonly IEmailSender _emailSender;

        public StoreAdminController(IWebHostEnvironment p, IEmailSender emailSender)
        {
            _enviro = p;
            _emailSender = emailSender;
        }

        public IActionResult List()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            var datas = from s in db.Stores.Include(a => a.AccountStatusNavigation)
                        select s;

            return View(datas);
        }

        public IActionResult Edit(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Store sto = db.Stores.FirstOrDefault(s => s.StoreId == id);
            if (sto == null)
            {
                return RedirectToAction("List");
            }
            return View(sto);
        }
        [HttpPost]
        public IActionResult Edit(Store s, IFormFile StoreImage)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Store sto = db.Stores.FirstOrDefault(sto => sto.StoreId == s.StoreId);
            if (sto != null)
            {
                sto.AccountName = s.AccountName;
                sto.RestaurantName = s.RestaurantName;
                sto.TaxId = s.TaxId;
                sto.RestaurantPhone = s.RestaurantPhone;
                sto.RestaurantAddress = s.RestaurantAddress;
                sto.StoreMail = s.StoreMail;
                sto.Website = s.Website;
                sto.Seats = s.Seats;
                sto.OpeningTime = s.OpeningTime;
                sto.ClosingTime = s.ClosingTime;
                sto.AccountStatus = s.AccountStatus;

                db.SaveChanges();
            }
            if (StoreImage != null)
            {
                string photoName = s.RestaurantName + ".jpg";
                string path = _enviro.WebRootPath + "/images/test/" + photoName;
                StoreImage.CopyTo(new FileStream(path, FileMode.Create));
            }

            return RedirectToAction("List");
        }

        public IActionResult Verify(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Store sto = db.Stores.FirstOrDefault(sto => sto.StoreId == id);
            if (sto != null && sto.AccountStatus != 1) // 檢查帳戶狀態是否已驗證
            {
                // 執行驗證邏輯

                // 發送驗證郵件給店家
                string recipientEmail = sto.StoreMail; // 收件者的郵箱地址
                string subject = "膳糧平台註冊店家驗證通知信"; // 郵件主題
                // 郵件內容
                string message = " 您好：<br><label>&emsp;&emsp;請於收到信件後，盡快進行驗證。</label><br>";

                _emailSender.SendEmailAsync(recipientEmail, subject, message, id);

            }
            return RedirectToAction("List");
        }

        //[HttpPost]
        public IActionResult CompleteVerification(int id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Store sto = db.Stores.FirstOrDefault(sto => sto.StoreId == id);
            if (sto != null && sto.AccountStatus != 1)
            {
                // 更新店家狀態為已驗證
                sto.AccountStatus = 1;
                db.SaveChanges();

                // 重新導向到店家登入畫面
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Index", "Home"); // 若更新失敗，則重定向到首頁
        }
    }
}
