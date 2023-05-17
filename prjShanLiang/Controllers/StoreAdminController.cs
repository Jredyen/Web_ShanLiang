using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class StoreAdminController : Controller
    {
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
            return RedirectToAction("List");
        }
    }
}
